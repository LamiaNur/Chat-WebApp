import {inject, NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './identity/components/register/register.component';
import { LogInComponent } from './identity/components/log-in/log-in.component';
import { UserProfileComponent } from './identity/components/user-profile/user-profile.component';
import { ContactComponent } from './contact/components/contact/contact.component';
import { AuthService } from "./identity/services/auth.service";
import { ChatComponent } from './chat/components/chat/chat.component';
import { ChatListComponent } from './chat/components/chat-list/chat-list.component';
import { HomeComponent } from './identity/components/home/home.component';

const routes: Routes = [
  {
    path : "register",
    component : RegisterComponent,
    canActivate: [() => !inject(AuthService).canActivate()]
  },
  {
    path : "log-in",
    component : LogInComponent,
    canActivate: [() => !inject(AuthService).canActivate()]
  },
  {
    path: "user/:type",
    component: UserProfileComponent,
    canActivate: [() => inject(AuthService).canActivate()]
  },
  {
    path: "contact",
    component: ContactComponent,
    canActivate: [() => inject(AuthService).canActivate()]
  },
  {
    path: "chat/:type",
    component: ChatComponent,
    canActivate:[() => inject(AuthService).canActivate()]
  },
  {
    path: "chat",
    component : ChatListComponent,
    canActivate:[() => inject(AuthService).canActivate()]
  },
  {
    path: "",
    component : HomeComponent
  },
  {
    path: "home",
    component : HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
