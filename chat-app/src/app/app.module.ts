import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './identity/components/register/register.component';
import { LogInComponent } from './identity/components/log-in/log-in.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommandService } from './core/services/command-service';
import { AuthService } from './identity/services/auth.service';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { QueryService } from './core/services/query-service';
import { UserProfileComponent } from './identity/components/user-profile/user-profile.component';
import { ContactComponent } from './contact/components/contact/contact.component';
import { UserService } from './identity/services/user.service';
import { AlertComponent } from './core/components/alert/alert.component';
import { AlertService } from './core/services/alert-service';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LogInComponent,
    UserProfileComponent,
    ContactComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    CommandService, 
    QueryService,
    AuthService,
    UserService, 
    AlertService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true} 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
