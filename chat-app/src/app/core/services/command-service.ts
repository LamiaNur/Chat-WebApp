import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CommandResponse } from "../models/command-response";

@Injectable({
  providedIn: 'root',
})
export class CommandService {
    
    constructor(
        private httpClient : HttpClient) {}
    
    public execute(command : any) {
        if (!command.apiUrl) {
            console.error("Api Url not set...");
        }
        return this.httpClient.post<CommandResponse>(command.apiUrl, command);
    }
}