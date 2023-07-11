import { CommandBase } from "src/app/core/models/command-base";
import { UserModel } from "../models/user-model";
import { Configuration } from "src/app/core/services/configuration";

export class RegisterCommand extends CommandBase {
    
    public userModel : UserModel | undefined;
    public publicKey : any;
    
    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/register");
    }
}