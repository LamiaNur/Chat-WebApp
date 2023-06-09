import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommandService } from 'src/app/core/services/command-service';
import { take, timestamp } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
  
  logInFormControl = this.fb.group({
    email : ['', [Validators.required, Validators.email]],
    password : ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor(
    private commandService: CommandService,
    private authService: AuthService,
    private userService: UserService,
    private router: Router,
    private fb: FormBuilder) {}

  ngOnInit() {
    
  }

  onSubmit() {
    localStorage.clear();
    var logInCommand = this.authService.getLogInCommand(this.getFormValue("email"), this.getFormValue("password"));
    this.commandService.execute(logInCommand).pipe(take(1)).subscribe(response => {
      console.log(response);
      if (response.status === ResponseStatus.success) {
        this.authService.setTokenToStore(response.metaData.Token);
        localStorage.setItem("email", this.getFormValue("email"));
        this.userService.getUserProfileByEmail(this.getFormValue("email"))
        .pipe(take(1))
        .subscribe(response => {
          if (response.status === ResponseStatus.success) {
            console.log("received user profile", response);
            localStorage.setItem('userProfile', JSON.stringify(response.items[0]));
            this.router.navigateByUrl("user-profile");
          }
        });
      }
    });
  }

  getFormValue(key : string) {
    return this.logInFormControl.get(key)?.value?.toString();
  }

}
