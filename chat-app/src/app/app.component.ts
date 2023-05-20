import { Component, OnInit } from '@angular/core';
import { UserProfileQuery } from './identity/queries/user-profile-query';
import { QueryService } from './core/services/query-service';
import { take } from 'rxjs';
import { ResponseStatus } from './core/constants/response-status';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'chat-app';
  
  constructor(private queryService : QueryService) {

    
  }
  ngOnInit(): void {
    
  }
}
