import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CommandResponse } from "../models/command-response";
import { Configuration } from "./configuration";
import { QueryBase } from "../models/query-base";
import { QueryService } from "./query-service";
import { map, pipe } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class FileService {
    /**
     *
     */
    constructor(
        private httpClient : HttpClient,
        private queryService: QueryService) {
    }

    uploadFile(file : any) {
        const api = Configuration.identityApi + "/file/upload";
        const formData = new FormData();
        formData.append('formFile', file, file.name);
        return this.httpClient.post<CommandResponse>(api, formData);
    }

    downloadFile(fileId: any) {
        const api = Configuration.identityApi + "/file/download?fileId=" + fileId;
        return this.httpClient.get(api, {responseType: 'blob'})
        .pipe(
            map(response => {
                const blob = new Blob([response], { type: 'image/jpeg' }); // Adjust the type according to your image format
                return URL.createObjectURL(blob);
            })
        );
    }
    
    getFileModelByFileId(fileId : any) {
        const fileModelQuery = new FileModelQuery();
        fileModelQuery.fileId = fileId;
        return this.queryService.execute(fileModelQuery);
    }
}

export class FileModelQuery extends QueryBase {
    
    fileId : any;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi + "/File/get";
    }

}