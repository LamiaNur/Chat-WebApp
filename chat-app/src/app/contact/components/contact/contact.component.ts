import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommandService } from 'src/app/core/services/command-service';
import { AuthService } from 'src/app/identity/services/auth.service';
import { AddContactCommand } from '../../commands/add-contact-command';
import { UserService } from 'src/app/identity/services/user.service';
import { take } from 'rxjs';
import { ResponseStatus } from 'src/app/core/constants/response-status';
import { ContactQuery } from '../../queries/contact-query';
import { QueryService } from 'src/app/core/services/query-service';
import { AcceptOrRejectContactRequestCommand } from '../../commands/accept-or-reject-contact-request-command';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  
  addContactFormControl = this.fb.group({
    email : ['', [Validators.required, Validators.email]],
  });
  
  items : any;
  contactTabs : any;
  activeTabIndex : any = 0;
  currentUserId : any;

  constructor(
    private userService : UserService,
    private commandService: CommandService,
    private queryService : QueryService,
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder) {}

  ngOnInit(): void {
    this.currentUserId = this.userService.getCurrentUserId();
    this.contactTabs = [
      {
        'id': 0,
        'key': "Contacts",
        'text': "Contacts",
        'isActive' : true
      },
      {
        'id' : 1,
        'key': "Requests",
        'text': "Requests",
        'isActive' : false
      },
      {
        'id' : 2,
        'key': "Pendings",
        'text': "Pendings",
        'isActive' : false
      }
    ];
    this.getContacts();
  }

  getContacts() {
    this.queryService.execute(this.getContactQuery())
    .pipe(take(1))
    .subscribe(response => {
      console.log(response);
      this.items = response.items;
    });
  }

  onSubmitAddContact() {
    this.commandService.execute(this.getAddContactCommand())
    .pipe(take(1))
    .subscribe(response => {
      alert(response.message);
    });
  }

  onClickTab(idx : any) {
    this.activeTabIndex = idx;
    for (let i = 0; i < this.contactTabs.length; i++) {
      if (i === idx) {
        this.contactTabs[i].isActive = true;
        this.getContacts();
        console.log("[ContactComponent] (onClickTab) selected index %d", idx, this.contactTabs[i]);
        continue;
      } 
      this.contactTabs[i].isActive = false;
    }
  }

  onClickContact(id : any) {
    this.commandService.execute(this.getAcceptOrRejectContactRequestCommand(id))
    .pipe(take(1))
    .subscribe(response => {
      alert(response.message);
    });
  }
  
  getAcceptOrRejectContactRequestCommand(id : any) {
    var acceptOrRejectContactRequestCommand = new AcceptOrRejectContactRequestCommand();
    acceptOrRejectContactRequestCommand.contactId = this.items[id].id;
    acceptOrRejectContactRequestCommand.isAcceptRequest = this.items[id].requestUserId !== this.currentUserId;
    return acceptOrRejectContactRequestCommand;
  }

  getAddContactCommand() {
    var addContactCommand = new AddContactCommand();
    addContactCommand.contactEmail = this.getFormValue("email");
    addContactCommand.userId = this.userService.getCurrentUserId();
    return addContactCommand;
  }

  getContactQuery() {
    var contactQuery = new ContactQuery();
    contactQuery.isPendingContacts = this.activeTabIndex === 2;
    contactQuery.isRequestContacts = this.activeTabIndex === 1;
    contactQuery.userId = this.userService.getCurrentUserId();
    return contactQuery;
  }

  getFormValue(key : string) {
    return this.addContactFormControl.get(key)?.value?.toString();
  }
}
