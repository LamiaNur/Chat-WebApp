import { CommandBase } from "src/app/core/models/command-base";
import { Configuration } from "src/app/core/services/configuration";

export class UpdateChatsStatusCommand extends CommandBase {
    
    userId : any;
    openedChatUserId : any;
    
    constructor() {
        super();
        this.apiUrl = Configuration.identityApi + "/chat/update-status"
    }
}