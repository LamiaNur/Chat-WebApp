import { QueryBase } from "src/app/core/models/query-base";
import { Configuration } from "src/app/core/services/configuration";

export class ChatListQuery extends QueryBase {
    
    userId : any;
    
    constructor() {
        super();
        this.apiUrl = Configuration.identityApi + "/chat/list";
        this.Offset = 0;
        this.limit = 100;
    }
}