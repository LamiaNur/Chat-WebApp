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
import { FileService } from 'src/app/core/services/file-service';
import { SecurtiyService } from 'src/app/core/services/security-service';
import { EncrytptionDecryptionFactory, IEncryptionDecryption } from 'src/app/core/helpers/encryption-decryption-helper';
import { ChatProcessor } from '../../helpers/chat-processor';

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
  canExecuteChatQuery: any = true;
  sendToUserBlobImageUrl: any = '';
  sharedSecret: any;
  encryptionDecryptionHelper: IEncryptionDecryption | undefined;

  constructor(
    private elementRef : ElementRef,
    private userService : UserService,
    private commandService : CommandService,
    private queryServie : QueryService,
    private signalRService: SignalRService,
    private chatSocketService: ChatSocketService,
    private fileService: FileService,
    private router : Router,
    private securityService: SecurtiyService) {}
  
  ngOnInit(): void {
    this.encryptionDecryptionHelper = EncrytptionDecryptionFactory.getEncryptionDecryption();
    this.chats = [];
    this.currentUserId = this.userService.getCurrentUserId();
    this.sendToUserId = this.userService.getCurrentOpenedChatUserId();
    this.currentUserProfile = this.userService.getCurrentUserProfile();

    this.query.sendTo = this.sendToUserId;
    this.query.userId = this.currentUserId;
    
    this.userService.getUserProfileById(this.sendToUserId)
    .pipe(take(1))
    .subscribe(res => {
      if (res.status === ResponseStatus.success) {
        this.sendToUserProfile = res.items[0];
        this.sharedSecret = this.securityService.getSharedSecretKey(this.sendToUserProfile.publicKey);
        this.chatTitle = this.sendToUserProfile.firstName + " " + this.sendToUserProfile.lastName;
        this.getChats(this.query);
        if (this.sendToUserProfile.profilePictureId){
          this.fileService.downloadFile(this.sendToUserProfile.profilePictureId)
          .pipe(take(1))
          .subscribe(response => {
            this.sendToUserBlobImageUrl = response;
            console.log("Blob url", response);
          });
        }
      }
    });
    this.chatSocketService.getChatSocketObservable()
    .subscribe(message => {
      message = ChatProcessor.process(message, this.sharedSecret);
      this.chats = [message].concat(this.chats);
      this.setChatScrollStartFromBottom();
    });
    this.getLastSeenStatus();
  }

  getChats(query : ChatQuery) {
    this.queryServie.execute(query)
    .pipe(take(1))
    .subscribe(res => {
      if (res.status === ResponseStatus.success) {
        if (res.items.length === 0) {
          this.canExecuteChatQuery = false;
        } else {
          this.chats = this.chats.concat(this.processChats(res.items));
          if (query.Offset === 0) {
            this.setChatScrollStartFromBottom();
            this.totalChats = res.totalCount;
          }
        }
      }
    });
  }

  processChats(chats : any) {
    for (let index = 0; index < chats.length; index++) {
      chats[index] = ChatProcessor.process(chats[index], this.sharedSecret);
    }
    return chats;
  }

  // processChat(chat : any) {
  //   const chatTime = new Date(chat.sentAt);
  //   const currentTime = new Date();
  //   if (chatTime.getDay() === currentTime.getDay()) {
  //     chat.sentAt = chatTime.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
  //   } else {
  //     chat.sentAt = chatTime.toLocaleDateString();
  //   }
  //   chat.message = this.encryptionDecryptionHelper?.decrypt(chat.message, this.sharedSecret);
  //   return chat;
  // }

  setChatScrollStartFromBottom() {
    timer(1).subscribe(res => {
      const chatContainer = this.elementRef.nativeElement.querySelector('.chat-container');
      chatContainer.scrollTop = chatContainer.scrollHeight - chatContainer.clientHeight;
    });
  }
  

  onChatScroll(event: any): void {
    console.log("clientHeight : " + event.target.clientHeight + "\nscrolltop : " + event.target.scrollTop + "\nscrollheight : " + event.target.scrollHeight);
    if( event.target.scrollTop < event.target.clientHeight && this.canExecuteChatQuery) {
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
    sendMessageCommand.chatModel.message = this.encryptionDecryptionHelper?.encrypt(sendMessageCommand.chatModel.message, this.sharedSecret);
    this.inputMessage = '';
    this.commandService.execute(sendMessageCommand)
    .pipe(take(1))
    .subscribe(response => {
      this.chats = [ChatProcessor.process(response.metaData.Message, this.sharedSecret)].concat(this.chats);
      this.setChatScrollStartFromBottom();
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

  onClickUserProfile() {
    const userId = this.sendToUserId;
    this.router.navigateByUrl('/user/' + userId);
  }
}
