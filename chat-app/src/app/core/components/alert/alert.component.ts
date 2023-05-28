import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert-service';
import { take, takeUntil } from 'rxjs';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  message : any = "A simple success alert—check it out!";
  alertType : any = "danger";
  isActiveAlert : any = false;

  constructor (private alertService: AlertService) {
  }
  
  ngOnInit(): void {
    
    this.alertService.getAlertObservable()
    .subscribe(data => {
      console.log("[AlertComponent] subscriber - data received", data);
      this.showAlert(data);
    });

  }

  onClickCloseAlert() {
    this.isActiveAlert = false;
  }

  showAlert(data : any) {
    this.isActiveAlert = true;
    this.alertType = data.alertType;
    this.message = data.message;
  }
}
