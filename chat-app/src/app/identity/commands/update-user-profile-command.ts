import { CommandBase } from "src/app/core/models/command-base";
import { UserModel } from "../models/user-model";
import { Configuration } from "src/app/core/services/configuration";

export class UpdateUserProfileCommand extends CommandBase {
    
    public userModel : UserModel | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/update");
    }
}