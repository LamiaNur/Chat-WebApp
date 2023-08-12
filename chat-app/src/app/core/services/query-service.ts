import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { QueryResponse } from "../models/query-response";
import { Subject, take } from "rxjs";
import { ResponseSubject } from "../models/response-subject";
import { AlertService } from "./alert-service";

@Injectable({
  providedIn: 'root',
})
export class QueryService {
    constructor(
        private httpClient : HttpClient,
        private alertService: AlertService) {}
    
    public execute(query : any) {
        return new ResponseSubject(query, this.alertService, this.httpClient).subject.asObservable();
    }
}