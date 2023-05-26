import { CommandBase } from "src/app/core/models/command-base";
import { Configuration } from "src/app/core/services/configuration";

export class AddContactCommand extends CommandBase {
    
    public userId : string | undefined;
    public contactEmail : string | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Contact/add");
    }
}