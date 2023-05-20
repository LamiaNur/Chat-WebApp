import { QueryBase } from "src/app/core/models/query-base";
import { Configuration } from "src/app/core/services/configuration";

export class LastSeenQuery extends QueryBase{
    
    userId : string | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Activity/last-seen");
    }
}