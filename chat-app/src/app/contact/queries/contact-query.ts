import { QueryBase } from "src/app/core/models/query-base";
import { Configuration } from "src/app/core/services/configuration";

export class ContactQuery extends QueryBase{
    
    userId : string | undefined | null;
    isPendingContacts : boolean | undefined;
    isRequestContacts : boolean | undefined;
    
    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Contact/get");
    }
}