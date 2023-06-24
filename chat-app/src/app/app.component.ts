import { Component, ElementRef, OnInit } from '@angular/core';
import { CommandService } from './core/services/command-service';
import { AuthService } from './identity/services/auth.service';
import { take } from 'rxjs';
import { ResponseStatus } from './core/constants/response-status';
import { Router } from '@angular/router';
import { AlertService } from './core/services/alert-service';
import * as signalR from "@microsoft/signalr";
import { SignalRService } from './core/services/signalr-service';
import { UserService } from './identity/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'chat-app';
  isLoggedIn : boolean = false;
  currentOpenedNavItem: any = '';
  constructor(
    private commandService : CommandService,
    private alertService : AlertService,
    private userService: UserService,
    private authService: AuthService,
    private signalRService : SignalRService,
    private router: Router) {}

  ngOnInit(): void {
    this.setCurrentOpenedNavItem();
    this.isLoggedIn = this.authService.isLoggedIn();
    if (this.isLoggedIn)
      this.signalRService.startConnection();
  }

  onClickLogOut() {
    this.commandService.execute(this.authService.getLogOutCommand())
    .pipe(take(1))
    .subscribe(response => {
      if (response.status) {
        this.authService.logOut(); 
        this.isLoggedIn = this.authService.isLoggedIn();
        this.router.navigateByUrl("log-in");
      }
    });
  }

  setCurrentOpenedNavItem() {
    if (this.router.url.includes('chat')) {
      this.currentOpenedNavItem = 'chat';
    }
    else if (this.router.url.includes('contact')) {
      this.currentOpenedNavItem = 'contact';
    }
    else if (this.router.url.includes('home')) {
      this.currentOpenedNavItem = 'home';
    }
    else {
      this.currentOpenedNavItem =  '';
    }
  }

  onClickNavItem(item: any) {
    this.currentOpenedNavItem = item;
    this.router.navigateByUrl(item);
  }

  onClickUserProfile() {
    const userId = this.userService.getCurrentUserId();
    this.router.navigateByUrl('/user/' + userId);
  }
}
