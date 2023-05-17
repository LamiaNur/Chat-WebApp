import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommandService } from 'src/app/core/services/command-service';
import { LoginCommand } from '../../commands/login-command';
import { take } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';

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
    private router: Router,
    private fb: FormBuilder) {}

  ngOnInit() {
    
  }

  onSubmit() {
    var logInCommand = this.getLogInCommand();
    this.commandService.execute(logInCommand).pipe(take(1)).subscribe(response => {
      console.log(response);
      if (response.status === ResponseStatus.success) {
        this.router.navigateByUrl("");
      } else {
        alert(response.message);
      }
    });
  }

  getLogInCommand() {
    var logInCommand = new LoginCommand();
    logInCommand.email = this.getFormValue("email");
    logInCommand.password = this.getFormValue("password");
    logInCommand.appId = "1234";
    return logInCommand;
  }

  getFormValue(key : string) {
    return this.logInFormControl.get(key)?.value?.toString();
  }
}
