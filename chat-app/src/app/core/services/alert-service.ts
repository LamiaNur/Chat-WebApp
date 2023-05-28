import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class AlertService{
    
    private alertSubject : Subject<any> = new Subject<any>();

    getAlertObservable() {
        return this.alertSubject.asObservable();
    }

    showAlert(message : any, alertType : any) {
        const alertData = {
            'alertType' : alertType,
            'message' : message
        };
        this.alertSubject.next(alertData);
    }
}