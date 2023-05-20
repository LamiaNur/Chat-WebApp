import { QueryBase } from "src/app/core/models/query-base";
import { Configuration } from "src/app/core/services/configuration";

export class UserProfileQuery extends QueryBase{
    
    email : string | undefined | null;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/user-profile");
    }
}