import { Component, OnInit } from '@angular/core';
import { CommandService } from './core/services/command-service';
import { AuthService } from './identity/services/auth.service';
import { take } from 'rxjs';
import { ResponseStatus } from './core/constants/response-status';
import { Router } from '@angular/router';
import { AlertService } from './core/services/alert-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'chat-app';
  isLoggedIn : boolean = false;

  constructor(
    private commandService : CommandService,
    private alertService : AlertService,
    private authService: AuthService,
    private router: Router) {}

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  onClickLogOut() {
    this.commandService.execute(this.authService.getLogOutCommand())
    .pipe(take(1))
    .subscribe(response => {
      if (response.status) {
        this.authService.logOut(); 
        this.isLoggedIn = this.authService.isLoggedIn();
        this.router.navigateByUrl("log-in");
        //this.alertService.showAlert(response.message, "success");
      }
    });
  }
}
