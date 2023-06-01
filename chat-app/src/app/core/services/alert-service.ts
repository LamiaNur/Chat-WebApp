import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import {MatSnackBar, MatSnackBarConfig} from "@angular/material/snack-bar";
import {AlertComponent} from "../components/alert/alert.component";

@Injectable({
    providedIn: 'root',
})
export class AlertService {
  
  constructor(private _snackBar: MatSnackBar) {}

  showAlert(message : any, alertType : any, duration : any = 3000) {
    this._snackBar.openFromComponent(AlertComponent, {
      duration: this.getDuration(message, duration),
      data : message,
      verticalPosition : "top",
      horizontalPosition : "center",
      panelClass : [alertType + '-snackbar']
    });
  }

  getDuration(message : any, duration : any) {
    var messageLenDuration : any = (message.length / 10) * 1000;
    if (duration === 3000) {
      return messageLenDuration >= duration? messageLenDuration : duration;
    }
    return duration;
  }
}
