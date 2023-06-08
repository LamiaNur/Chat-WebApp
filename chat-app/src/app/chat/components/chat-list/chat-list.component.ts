import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/core/services/query-service';
import { ChatListQuery } from '../../queries/chat-list-query';
import { UserService } from 'src/app/identity/services/user.service';
import { take } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit{
  
  items : any;
  chatList : any;

  constructor(
    private queryService : QueryService, 
    private userService : UserService,
    private router : Router) {
    
  }
  
  ngOnInit(): void {
    var chatLstQuery = new ChatListQuery();
    chatLstQuery.userId = this.userService.getCurrentUserId();
   this.queryService.execute(chatLstQuery)
   .pipe(take(1))
   .subscribe(response => {
    this.items = response.items;
    this.setChatList();
   });
  }

  setChatList() {
    if (!this.items || this.items.length === 0) return;
    this.chatList = [];
    for (let i = 0; i < this.items.length; i++) {
      let chat = {
        'latestMessage' : this.items[i].message,
        'sentAt' : this.items[i].sentAt,
        'chatName' : "LOL",
        'userId' : this.items[i].userId,
        'sendTo': this.items[i].sendTo
      };
      this.chatList.push(chat);
    } 
    console.log(this.chatList);
  }

  onClickChat(chat: any) {
    console.log("clicked", chat);
    var id = chat.userId === this.userService.getCurrentUserId()? chat.sendTo : chat.userId;
    var url = "chat/" + id;
    this.router.navigateByUrl(url);
  }

}
