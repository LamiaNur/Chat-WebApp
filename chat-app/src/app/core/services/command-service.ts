import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CommandResponse } from "../models/command-response";
import { AlertService } from "./alert-service";
import { Subject, take } from "rxjs";
import { ResponseStatus } from "../constants/response-status";
import { ResponseSubject } from "../models/response-subject";

@Injectable({
  providedIn: 'root',
})
export class CommandService {

    constructor(
        private httpClient : HttpClient,
        private alertService : AlertService) {}

    public execute(command : any) {
        return new ResponseSubject(
            command,this.alertService, 
            this.httpClient).subject.asObservable();
    }
}
