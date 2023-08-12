import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthService } from "../identity/services/auth.service";
import { CommandService } from "../core/services/command-service";
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from "rxjs";
import { ResponseStatus } from "../core/constants/response-status";
import { Configuration } from "../core/services/configuration";
import { CommandResponse } from "../core/models/command-response";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  
  constructor(
    private authService: AuthService,
    private commandService: CommandService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.shouldInclude(req)) return next.handle(req);
    let token: any = this.authService.getAccessToken();
    if (!token) return next.handle(req);
    
    console.log("[AuthInterceptor] token added to header");
    req = this.addTokenToHeader(req, token);
    
    return next.handle(req).pipe(
      catchError(error => {
      console.log("[AuthInterceptor]", error);
      if (error instanceof HttpErrorResponse && this.shouldInclude(req) && error.status === 401) {
        return this.handle401Error(req, next);
      }
      return throwError(() => error);
    }));
  }
  
  addTokenToHeader(req: HttpRequest<any>, token : string) {
    return req.clone({
      headers: req.headers.set('Authorization', "Bearer " + token)
    });
  }
  
  shouldInclude(req: HttpRequest<any>) {
    if (req.url.startsWith(Configuration.identityApi+"/Auth/user-profile")) return true;
    if (req.url.startsWith(Configuration.identityApi+"/Auth/update")) return true;
    if (req.url.startsWith(Configuration.identityApi+"/Auth")) return false;
    return true;
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    console.log("[AuthInterceptor] handling 401 Error");
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      const token = this.authService.getRefreshToken();

      if (token) {
        var refreshTokenCommand = this.authService.getRefreshTokenCommand();
        return this.commandService.execute(refreshTokenCommand).pipe(
          switchMap((response: CommandResponse) => {
            console.log(response);
            this.isRefreshing = false;
            if (response.status == ResponseStatus.success) {
              this.authService.setTokenToStore(response.metaData.Token);
              this.refreshTokenSubject.next(this.authService.getAccessToken());
              return next.handle(this.addTokenToHeader(request, response.metaData.Token.accessToken));
            } 
            return throwError(() => response.message);
          }),
          catchError((err) => {
            this.isRefreshing = false;
            return throwError(() => err);
          })
        );
      }
    }

    return this.refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap((token) => next.handle(this.addTokenToHeader(request, token)))
    );
  }
}