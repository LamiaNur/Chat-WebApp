import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from 'src/app/identity/services/auth.service';
import { AlertService } from './alert-service';
import { Subject } from 'rxjs';

@Injectable()
export class SignalRService {
  private hubConnection: any;
  
  private notificationSubject : Subject<any> = new Subject<any>();

  constructor(private authService: AuthService,
    private alertService: AlertService) { }

  startConnection(): void {
    const token = this.authService.getAccessToken();
    const options : any = {
        accessTokenFactory : () => token
    };
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:50501/chatHub", options)
    .build();
    this.hubConnection.start().catch((err: string) => document.write(err));

    this.hubConnection.on("ReceivedChat",  (message: any) => {
        console.log("Received message with web socket", message);
        this.notificationSubject.next(message);
        // this.alertService.showAlert(JSON.stringify(message), 'success', 1000);
    });
  }
  getNotificationObservable() {
    return this.notificationSubject.asObservable();
  }
}
