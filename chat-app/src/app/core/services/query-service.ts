import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { QueryResponse } from "../models/query-response";

@Injectable({
  providedIn: 'root',
})
export class QueryService {
    
    constructor(
        private httpClient : HttpClient) {}
    
    public execute(query : any) {
        if (!query.apiUrl) {
            console.error("Api Url not set...");
        }
        return this.httpClient.post<QueryResponse>(query.apiUrl, query);
    }
}