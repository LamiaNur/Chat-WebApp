import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import {MatSnackBar, MatSnackBarConfig} from "@angular/material/snack-bar";
import {AlertComponent} from "../components/alert/alert.component";

@Injectable({
    providedIn: 'root',
})
export class AlertService {
  durationInSeconds = 5;

  constructor(private _snackBar: MatSnackBar) {}

  showAlert(message : any, alertType : any) {
    this._snackBar.openFromComponent(AlertComponent, {
      duration: this.durationInSeconds * 1000,
      data : message,
      verticalPosition : "top",
      horizontalPosition : "center",
      panelClass : [alertType + '-snackbar']
    });
  }
}
