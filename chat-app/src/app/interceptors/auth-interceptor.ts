import { HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthService } from "../identity/services/auth.service";
import { CommandService } from "../core/services/command-service";
import { take } from "rxjs";
import { ResponseStatus } from "../core/constants/response-status";
import { Configuration } from "../core/services/configuration";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private commandService: CommandService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.shouldInclude(req)) {
        return next.handle(req);
    }
    console.log("[AuthInterceptor] intercept");
    let token: any = this.authService.getAccessToken();
    if (token && this.authService.isTokenExpired(token)) {
        var refreshTokenCommand = this.authService.getRefreshTokenCommand();
        if (refreshTokenCommand.apiUrl != req.url)
        {
            this.commandService.execute(refreshTokenCommand)
            .pipe(take(1))
            .subscribe(response => {
                console.log(response);
                if (response.status === ResponseStatus.success) {
                this.authService.setTokenToStore(response.metaData.Token);
                } else {
                alert(response.message);
                }
            });
            token = this.authService.getAccessToken();
        }
    }
    if (!token) {
        token = "";
    }
    const authReq = req.clone({
      headers: req.headers.set('Authorization', token)
    });
    return next.handle(authReq);
  }

  shouldInclude(req: HttpRequest<any>) {
    if (req.url.startsWith(Configuration.identityApi+"/Auth")) return false;
    return true;
  }
}