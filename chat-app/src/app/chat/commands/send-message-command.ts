import { CommandBase } from "src/app/core/models/command-base";
import { Configuration } from "src/app/core/services/configuration";
import { ChatModel } from "../models/chat-model";

export class SendMessageCommand extends CommandBase {
    
    chatModel : ChatModel;
    
    constructor() {
        super();
        this.chatModel = new ChatModel();
        this.apiUrl = Configuration.identityApi + "/chat/send"
    }
}