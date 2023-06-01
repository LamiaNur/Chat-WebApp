import {Component, inject, OnInit} from '@angular/core';
import {MatSnackBarConfig, MatSnackBarRef} from "@angular/material/snack-bar";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {

  snackBarRef = inject(MatSnackBarRef);
  config : MatSnackBarConfig = new MatSnackBarConfig<any>();
  
  constructor () {}

  ngOnInit(): void {
    this.config = this.snackBarRef.containerInstance.snackBarConfig;
  }

}
