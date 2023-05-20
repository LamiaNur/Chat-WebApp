import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/core/services/query-service';
import { UserProfileQuery } from '../../queries/user-profile-query';
import { take } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { UserProfile } from '../../models/user-profile';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit{

  userProfile : UserProfile = new UserProfile();
  
  constructor(
    private queryService : QueryService) {}
    
  ngOnInit(): void {
    var userProfileQuery = new UserProfileQuery();
    userProfileQuery.email = "lamianur@gmail.com";
    this.queryService.execute(userProfileQuery)
    .pipe(take(1))
    .subscribe(response => {
      console.log(response);
      if (response.status === ResponseStatus.success) {
        this.userProfile = response.items[0];
      } else {

      }
    });
  }
}
