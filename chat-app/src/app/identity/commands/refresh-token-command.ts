import { CommandBase } from "src/app/core/models/command-base";
import { Token } from "../models/token";
import { Configuration } from "src/app/core/services/configuration";

export class RefreshTokenCommand extends CommandBase{
    
    token : Token | undefined;
    appId : string | undefined;
    
    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/refresh-token");
    }
}