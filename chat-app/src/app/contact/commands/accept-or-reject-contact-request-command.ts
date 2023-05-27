import { CommandBase } from "src/app/core/models/command-base";
import { Configuration } from "src/app/core/services/configuration";

export class AcceptOrRejectContactRequestCommand extends CommandBase{
    
    public contactId : string | undefined;
    public isAcceptRequest : boolean | undefined;

    constructor() {
        super();
        this.apiUrl = Configuration.identityApi.concat("/Contact/accept-reject");
    }
}