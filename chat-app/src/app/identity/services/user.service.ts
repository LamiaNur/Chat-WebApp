import { Injectable } from "@angular/core";
import { UserProfile } from "../models/user-profile";

@Injectable({
    providedIn: 'root',
})
export class UserService{
    constructor () {}

    getCurrentUserProfile() {
        const user = sessionStorage.getItem("userProfile");
        var userProfile = new UserProfile();
        if (user === null) return userProfile;
        userProfile = JSON.parse(user);
        return userProfile;
    }

    getCurrentUserId() {
        return this.getCurrentUserProfile().id;
    }
}