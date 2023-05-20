import { Injectable } from '@angular/core';
import { LoginCommand } from '../commands/login-command';
import { RefreshTokenCommand } from '../commands/refresh-token-command';
import { Token } from '../models/token';
@Injectable({
    providedIn: 'root',
})
export class AuthService {

    isTokenExpired(token: string) { 
        const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
        return (Math.floor((new Date).getTime() / 1000)) >= expiry;
    }

    getAccessToken() {
        return localStorage.getItem("accessToken");
    }

    getRefreshToken() {
        return localStorage.getItem("refreshToken");
    }
    
    setTokenToStore(token: any) {
        localStorage.setItem("accessToken", token.accessToken);
        localStorage.setItem("refreshToken", token.refreshToken);
    }
    
    removeAccessToken() {
        localStorage.removeItem("accessToken");
    }

    removeRefreshToken() {
        localStorage.removeItem("refreshToken");
    }

    getAppId() {
        var appId = localStorage.getItem("appId");
        if (appId) return appId;
        return this.setAppId();
    }
    
    getRefreshTokenCommand() { 
        var refreshTokenCommand = new RefreshTokenCommand();
        refreshTokenCommand.appId = this.getAppId();
        var token = new Token();
        token.accessToken = this.getAccessToken();
        token.refreshToken = this.getRefreshToken();
        refreshTokenCommand.token = token;
        return refreshTokenCommand;
    }
    
    getLogInCommand(email : string, password : string) {
        var logInCommand = new LoginCommand();
        logInCommand.email = email;
        logInCommand.password = password;
        logInCommand.appId = this.getAppId();
        return logInCommand;
    }

    private setAppId() {
        var appId = "1234";
        localStorage.setItem("appId", appId);
        return appId;
    }
}