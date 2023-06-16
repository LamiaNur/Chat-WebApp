import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/core/services/query-service';
import { UserProfileQuery } from '../../queries/user-profile-query';
import { take } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { UserProfile } from '../../models/user-profile';
import { CommandService } from 'src/app/core/services/command-service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

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
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder) {}
    
  ngOnInit(): void {
    console.log("[UserProfileComponent] ngOnInit");
    this.getUserProfile();
  }
  
  getUserProfile() {
    this.userProfile = this.userService.getCurrentUserProfile();
    this.setFormData();
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
  onFileSelected($event: any) {
    console.log($event);
    const file:File = $event.target.files[0];
  }
}
