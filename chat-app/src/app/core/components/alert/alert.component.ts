import {Component, inject, Inject, OnInit} from '@angular/core';
import { AlertService } from '../../services/alert-service';
import { take, takeUntil } from 'rxjs';
import {MAT_SNACK_BAR_DATA, MatSnackBarConfig, MatSnackBarRef} from "@angular/material/snack-bar";
import {MatButtonModule} from "@angular/material/button";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {

  // message : any = "A simple success alert—check it out!";
  // alertType : any = "danger";
  // isActiveAlert : any = false;
  snackBarRef = inject(MatSnackBarRef);
  config : MatSnackBarConfig = new MatSnackBarConfig<any>();
  constructor (private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.config = this.snackBarRef.containerInstance.snackBarConfig;
    // this.alertService.getAlertObservable()
    // .subscribe(data => {
    //   console.log("[AlertComponent] subscriber - data received", data);
    //   this.showAlert(data);
    // });

  }

  // onClickCloseAlert() {
  //   this.isActiveAlert = false;
  // }
  //
  // showAlert(data : any) {
  //   this.isActiveAlert = true;
  //   this.alertType = data.alertType;
  //   this.message = data.message;
  // }
}
