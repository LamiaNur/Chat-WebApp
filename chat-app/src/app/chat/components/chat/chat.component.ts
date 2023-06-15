import { AfterViewInit, Component, ElementRef, OnInit, Pipe, PipeTransform } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/identity/services/user.service';
import { SendMessageCommand } from '../../commands/send-message-command';
import { ChatModel } from '../../models/chat-model';
import { CommandService } from 'src/app/core/services/command-service';
import { take, takeLast, takeUntil, timer } from 'rxjs';
import { QueryService } from 'src/app/core/services/query-service';
import { ChatQuery } from '../../queries/chat-query';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { LastSeenQuery } from 'src/app/activity/queries/last-seen-query';
import { Subject } from '@microsoft/signalr';
import { SignalRService } from 'src/app/core/services/signalr-service';
import { ChatSocketService } from '../../services/chat-socket-service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit{
  
  chatTitle : any = "";
  lastSeen : any = "";
  inputMessage : any = "";
  currentUserId : any = "";
  currentUserProfile : any;
  sendToUserProfile : any;
  sendToUserId : any = "";
  chats : any;
  isActive : any; 
  query: ChatQuery = new ChatQuery();
  totalChats:any;

  constructor(
    private elementRef : ElementRef,
    private userService : UserService,
    private commandService : CommandService,
    private queryServie : QueryService,
    private signalRService: SignalRService,
    private chatSocketService: ChatSocketService,
    private router : Router) {}
  
  ngOnInit(): void {
    this.chats = [];
    this.currentUserId = this.userService.getCurrentUserId();
    this.sendToUserId = this.userService.getCurrentOpenedChatUserId();
    this.currentUserProfile = this.userService.getCurrentUserProfile();

    this.query.sendTo = this.sendToUserId;
    this.query.userId = this.currentUserId;
    this.getChats(this.query);
    
    this.userService.getUserProfileById(this.sendToUserId)
    .pipe(take(1))
    .subscribe(res => {
      if (res.name === "UserProfileQuery") {
        this.sendToUserProfile = res.items[0];
        this.chatTitle = this.sendToUserProfile.firstName + " " + this.sendToUserProfile.lastName;
        console.log("this is from user profile query Response",res);
      }
    });
    this.chatSocketService.getChatSocketObservable()
    .subscribe(message => {
      message = this.processChat(message);
      this.chats = [message].concat(this.chats);
      // this.processChats();
      this.setChatScrollStartFromBottom();
    });
    this.getLastSeenStatus();
  }

  getChats(query : ChatQuery) {
    this.queryServie.execute(query)
    .pipe(take(1))
    .subscribe(res => {
      if (res.status === ResponseStatus.success) {
        this.chats = this.chats.concat(res.items);
        this.processChats();
        if (query.Offset === 0) {
          this.setChatScrollStartFromBottom();
          this.totalChats = res.totalCount;
        }
        console.log("this is from get chat query Response",res);
        
      }
      console.log(res);
    });
  }

  processChats() {
    let totalLen = this.query.Offset + this.query.limit;
    if (totalLen > this.chats.length) totalLen = this.chats.length;
    for (let index = this.query.Offset; index < totalLen; index++) {
      this.chats[index] = this.processChat(this.chats[index]);
    }
    console.log(this.chats);
  }

  processChat(chat : any) {
    const chatTime = new Date(chat.sentAt);
    const currentTime = new Date();
    if (chatTime.getDay() === currentTime.getDay()) {
      chat.sentAt = chatTime.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
    } else {
      chat.sentAt = chatTime.toLocaleDateString();
    }
    return chat;
  }

  setChatScrollStartFromBottom() {
    timer(1).subscribe(res => {
      const chatContainer = this.elementRef.nativeElement.querySelector('.chat-container');
      chatContainer.scrollTop = chatContainer.scrollHeight - chatContainer.clientHeight;
      console.log(chatContainer.scrollTop);
    });
  }
  

  onChatScroll(event: any): void {
    console.log("clientHeight : " + event.target.clientHeight + "\nscrolltop : " + event.target.scrollTop + "\nscrollheight : " + event.target.scrollHeight);
    if( event.target.scrollTop < event.target.clientHeight) {
      this.getChats(this.query.getNextPaginationQuery());
    }
  }

  onClickSendMessage() {
    console.log(this.inputMessage);
    var sendMessageCommand = new SendMessageCommand();
    sendMessageCommand.chatModel.userId = this.currentUserId;
    sendMessageCommand.chatModel.sendTo = this.sendToUserId;
    sendMessageCommand.chatModel.message = this.inputMessage;
    sendMessageCommand.chatModel.status = "Sent";
    this.inputMessage = '';
    this.commandService.execute(sendMessageCommand)
    .pipe(take(1))
    .subscribe(response => {
      // this.getChats(this.query);
      // this.chats = [this.processChat(response.metaData.Message)].concat(this.chats);
      // this.processChats();
      // this.setChatScrollStartFromBottom();
    });
  }

  getLastSeenStatus() {
    var lastSeenQuery = new LastSeenQuery();
    lastSeenQuery.userIds = [this.sendToUserId];
    this.queryServie.execute(lastSeenQuery)
    .pipe(take(1))
    .subscribe(response => {
      this.lastSeen = response.items[0].status;
      this.isActive = response.items[0].isActive;
    });
  }
}
