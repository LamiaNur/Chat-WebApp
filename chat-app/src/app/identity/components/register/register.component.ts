import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../models/user-model';
import { RegisterCommand } from '../../commands/register-command';
import { CommandService } from 'src/app/core/services/command-service';
import { take } from 'rxjs';
import { FormBuilder, Validators } from '@angular/forms';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  
  registerFormControl = this.fb.group({
    firstName : ['', [Validators.required, Validators.pattern('[a-zA-z ]*')]],
    lastName : ['', [Validators.required, Validators.pattern('[a-zA-z ]*')]],
    birthDay : ['', Validators.required],
    about : [''],
    email : ['', [Validators.required, Validators.email]],
    password : ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword : ['', [Validators.required, Validators.minLength(6)]],
  });

  constructor(
    private commandService: CommandService,
    private router: Router,
    private fb: FormBuilder) {}

  ngOnInit() {
    
  }

  onSubmit() {
    var registerCommand = this.getRegisterCommand();
    this.commandService.execute(registerCommand).pipe(take(1)).subscribe(response => {
      console.log(response);
      if (response.status === ResponseStatus.success) {
        this.router.navigateByUrl("log-in");
      }
    });
  }

  getRegisterCommand() {
    var userModel = new UserModel();
    userModel.firstName = this.getFormValue('firstName');
    userModel.lastName = this.getFormValue('lastName');
    userModel.birthDay = this.getFormValue('birthDay');
    userModel.about = this.getFormValue('about');
    userModel.email = this.getFormValue('email');
    userModel.password = this.getFormValue('password');
    var registerCommand = new RegisterCommand();
    registerCommand.userModel = userModel;
    return registerCommand;
  }

  getFormValue(key : string) {
    return this.registerFormControl.get(key)?.value?.toString();
  }
}
