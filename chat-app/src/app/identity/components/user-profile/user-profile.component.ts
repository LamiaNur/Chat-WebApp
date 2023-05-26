import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/core/services/query-service';
import { UserProfileQuery } from '../../queries/user-profile-query';
import { take } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { UserProfile } from '../../models/user-profile';
import { CommandService } from 'src/app/core/services/command-service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit{
  
  isEditable : boolean = false;
  editButtonText : string = "Edit Profile";
  userProfile : UserProfile = new UserProfile();
  
  userProfileFormControl = this.fb.group({
    firstName : ['', [Validators.required, Validators.pattern('[a-zA-z ]*')]],
    lastName : ['', [Validators.required, Validators.pattern('[a-zA-z ]*')]],
    birthDay : ['', Validators.required],
    about : ['', Validators.maxLength(200)],
    email : ['', [Validators.required, Validators.email]]
  });

  constructor(
    private queryService : QueryService,
    private commandService: CommandService,
    private router: Router,
    private fb: FormBuilder) {}
    
  ngOnInit(): void {
    console.log("[UserProfileComponent] ngOnInit");
    let user = sessionStorage.getItem("userProfile");
    if (!user) {
      this.getUserProfile();
    } else {
      this.userProfile = JSON.parse(user);
      console.log("[UserProfileComponent] using session data", this.userProfile);
      this.setFormData();
    }
  }
  
  getUserProfile() {
    var userProfileQuery = new UserProfileQuery();
    userProfileQuery.email = localStorage.getItem("email");
    this.queryService.execute(userProfileQuery)
    .pipe(take(1))
    .subscribe(response => {
      console.log("[UserProfileComponent] UserProfileQuery", response);
      if (response.status === ResponseStatus.success) {
        this.userProfile = response.items[0];
        console.log("[UserProfileComponent] setting user profile to session", this.userProfile);
        sessionStorage.setItem("userProfile", JSON.stringify(this.userProfile));
        this.setFormData();
      } 
    });
  }

  onSubmit() {
    console.log("[UserProfileComponent] onSubmit");
  }

  onClickEditProfile() {
    console.log("[UserProfileComponent] onClickEditProfile");
    this.isEditable = !this.isEditable;
    if (this.isEditable) this.editButtonText = "Cancel";
    else this.editButtonText = "Edit Profile";
  }

  setFormData() {
    console.log("[UserProfileComponent] setFormData");
    this.userProfileFormControl.setValue({
      firstName: this.userProfile.firstName,
      lastName: this.userProfile.lastName,
      birthDay: this.userProfile.birthDay.split('T')[0],
      about: this.userProfile.about,
      email: this.userProfile.email
    });
  }
}
