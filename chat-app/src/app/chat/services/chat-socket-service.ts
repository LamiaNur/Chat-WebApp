import { Injectable, OnInit } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class ChatSocketService {
    
    chatSubject : Subject<any> = new Subject<any>();
    
    public getChatSocketObservable() {
        return this.chatSubject.asObservable();
    }
}