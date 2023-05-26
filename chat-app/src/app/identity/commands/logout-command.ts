import { CommandBase } from "src/app/core/models/command-base";
import { Configuration } from "src/app/core/services/configuration";

export class LogOutCommand extends CommandBase {
    public appId : string | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Auth/log-out");
    }
}