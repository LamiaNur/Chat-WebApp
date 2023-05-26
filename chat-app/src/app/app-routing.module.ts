import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './identity/components/register/register.component';
import { LogInComponent } from './identity/components/log-in/log-in.component';
import { UserProfileComponent } from './identity/components/user-profile/user-profile.component';
import { ContactComponent } from './contact/components/contact/contact.component';

const routes: Routes = [
  {path : "register", component : RegisterComponent},
  {path : "log-in", component : LogInComponent},
  {path: "user-profile", component: UserProfileComponent},
  {path: "contact", component: ContactComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
