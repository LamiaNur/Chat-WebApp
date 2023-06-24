import { HttpClient } from "@angular/common/http";
import { Subject, take } from "rxjs";
import { ResponseStatus } from "../constants/response-status";
import { AlertService } from "../services/alert-service";

export class ResponseSubject{
    public subject : Subject<any> = new Subject<any>();

    constructor(
        private request : any, 
        private alertService: AlertService,
        private httpClient : HttpClient) {
        this.sendRequest(request);
    }
    
    private sendRequest(request : any) {
        if (!request.apiUrl) {
            console.error("Api Url not set...");
            return;
        }
        console.log("Sending Request", request);
        this.httpClient.post<any>(request.apiUrl, request)
        .pipe(take(1))
        .subscribe(response => {
            console.log("Response Received", response);
            let alertType = 'error';
            if (response.status === ResponseStatus.success) {
                alertType = 'success';
            }
            this.alertService.showAlert(response.message, alertType);
            this.subject.next(response);
        });
    }
}