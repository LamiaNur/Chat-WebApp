import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CommandResponse } from "../models/command-response";
import { AlertService } from "./alert-service";
import { Subject, take } from "rxjs";
import { ResponseStatus } from "../constants/response-status";

@Injectable({
  providedIn: 'root',
})
export class CommandService {

    private commandSubject : Subject<any> = new Subject<any>();

    constructor(
        private httpClient : HttpClient,
        private alertService : AlertService) {}

    public execute(command : any) {
        if (!command.apiUrl) {
            console.error("Api Url not set...");
        }
        this.httpClient.post<CommandResponse>(command.apiUrl, command)
        .pipe(take(1))
        .subscribe(res => {
            let alertType = 'error';
            if (res.status === ResponseStatus.success) {
                alertType = 'success';
            }
            this.alertService.showAlert(res.message, alertType);
            this.commandSubject.next(res);
        });
        return this.commandSubject.asObservable();
    }
}
