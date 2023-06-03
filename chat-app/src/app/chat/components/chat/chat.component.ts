import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/identity/services/user.service';
import { SendMessageCommand } from '../../commands/send-message-command';
import { ChatModel } from '../../models/chat-model';
import { CommandService } from 'src/app/core/services/command-service';
import { take, takeLast } from 'rxjs';
import { QueryService } from 'src/app/core/services/query-service';
import { ChatQuery } from '../../queries/chat-query';
import { ResponseStatus } from 'src/app/core/constants/response-status';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit{
  
  chatTitle : any = "khairul anam mubin";
  lastSeen : any = "Active Now";
  inputMessage : any = "";
  currentUserId : any = "";
  sendToUserId : any = "";
  chats : any;

  constructor(
    private elementRef : ElementRef,
    private userService : UserService,
    private commandService : CommandService,
    private queryServie : QueryService,
    private router : Router) {}
  
  ngOnInit(): void {
    this.setChatScrollStartFromBottom();
    this.currentUserId = this.userService.getCurrentUserId();
    this.sendToUserId = this.userService.getCurrentOpenedChatUserId();
    var query = new ChatQuery();
    query.sendTo = this.sendToUserId;
    query.userId = this.currentUserId;

    this.queryServie.execute(query)
    .pipe(take(1))
    .subscribe(res => {
      if (res.status === ResponseStatus.success) {
        this.chats = res.items;
      }
      console.log(res);
    });
  }

  setChatScrollStartFromBottom() {
    const chatContainer = this.elementRef.nativeElement.querySelector('.chat-container');
    chatContainer.scrollTop = chatContainer.scrollHeight;
  }

  onChatScroll(event: any): void {
    console.log(event);
    const element = event.target;
    const scrollHeight = element.scrollHeight;
    const scrollTop = element.scrollTop;
    const clientHeight = element.clientHeight;
  }

  onClickSendMessage() {
    console.log(this.inputMessage);
    var sendMessageCommand = new SendMessageCommand();
    sendMessageCommand.chatModel.userId = this.currentUserId;
    sendMessageCommand.chatModel.sendTo = this.sendToUserId;
    sendMessageCommand.chatModel.message = this.inputMessage;
    sendMessageCommand.chatModel.status = "new";
    this.inputMessage = '';
    this.commandService.execute(sendMessageCommand)
    .pipe(take(1))
    .subscribe(response => {
      this.router.navigateByUrl(this.router.url);
    });
  }
}
