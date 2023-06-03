import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { QueryResponse } from "../models/query-response";
import { Subject, take } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class QueryService {
    constructor(
        private httpClient : HttpClient) {}
    
    public execute(query : any) {
        return new ActualSubject(query, this.httpClient).subject.asObservable();
    }
}

export class ActualSubject{
    public subject : Subject<any> = new Subject<any>();

    constructor(
        private query : any,
        private httpClient : HttpClient) {
            this.execute(query);
        }
    
    private execute(query : any) {
        if (!query.apiUrl) {
            console.error("Api Url not set...");
        }
        console.log("Executing Query ", query);
        this.httpClient.post<QueryResponse>(query.apiUrl, query)
        .pipe(take(1))
        .subscribe(response => {
            console.log("Query Response Received", response);
            this.subject.next(response);
        });
    }
}