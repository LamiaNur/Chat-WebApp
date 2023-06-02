import { Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit{
  
  constructor(private elementRef : ElementRef) {}
  scrolledChats: number = 0;
  
  ngOnInit(): void {
    const chatContainer = this.elementRef.nativeElement.querySelector('.chat-container');
    chatContainer.scrollTop = chatContainer.scrollHeight;
  }

  onChatScroll(event: any): void {
    console.log(event);
    const element = event.target;
    const scrollHeight = element.scrollHeight;
    const scrollTop = element.scrollTop;
    const clientHeight = element.clientHeight;

    if (scrollTop === 0) {
      // User has scrolled to the top
      // Perform necessary action, e.g., load more chats
    }

    if (scrollTop + clientHeight === scrollHeight) {
      // User has scrolled to the bottom
      // Perform necessary action, e.g., mark all chats as read
    }

    // Calculate the number of chats scrolled
    this.scrolledChats = Math.floor(scrollTop / clientHeight);
    console.log(this.scrolledChats);
  }
}
