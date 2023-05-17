import { CommandBase } from "src/app/core/models/command-base";
import { UserModel } from "../models/user-model";
import { Configuration } from "src/app/core/services/configuration";

export class LoginCommand extends CommandBase {
    
    public email : string | undefined;
    public password : string | undefined;
    public appId : string | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/log-in");
    }
}