import { Component, ElementRef, OnInit } from '@angular/core';
import { CommandService } from './core/services/command-service';
import { AuthService } from './identity/services/auth.service';
import { take } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from './core/services/alert-service';
import { SignalRService } from './core/services/signalr-service';
import { UserService } from './identity/services/user.service';
import { MathHelper } from './core/helpers/math-helper';
import { DiffieHellmanKeyExchange } from './core/cryptography/diffie-hellman-key-exchange';
import { EncrytptionDecryptionFactory, IEncryptionDecryption } from './core/helpers/encryption-decryption-helper';
import { SecurtiyService } from './core/services/security-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  
  title = 'chat-app';
  isLoggedIn : boolean = false;
  currentOpenedNavItem: any = '';
  val : any = [];
  encryptionDecryption: IEncryptionDecryption;

  constructor(
    private commandService : CommandService,
    private alertService : AlertService,
    private userService: UserService,
    private authService: AuthService,
    private signalRService : SignalRService,
    private router: Router,
    private securityService : SecurtiyService) {
      this.encryptionDecryption = EncrytptionDecryptionFactory.getEncryptionDecryption();
    }

  ngOnInit(): void {
    this.setCurrentOpenedNavItem();
    this.isLoggedIn = this.authService.isLoggedIn();
    if (this.isLoggedIn)
      this.signalRService.startConnection();

    
    // var privateKey1 = this.securityService.createAndSavePrivateKey("123456");
    // var privateKey2 = this.securityService.createAndSavePrivateKey("123123");

    // var publicKey1 = this.securityService.getPublicKey(privateKey1);
    // var publicKey2 = this.securityService.getPublicKey(privateKey2);

    // var sharedSecred1 = this.securityService.getSharedSecretKey(publicKey1, privateKey2);
    // var sharedSecred2 = this.securityService.getSharedSecretKey(publicKey2, privateKey1);

    // console.log("=================================");
    // console.log(sharedSecred1);
    // console.log(sharedSecred2);
    // console.log("=================================");
    // const client2 = new DiffieHellmanKeyExchange();
    // client2.initialize(client1.prime, client1.alpha);
    // client2.generatePrivateKey();
    // client2.calculatePublicKey();

    // let secretClient1 = client1.calculateSharedSecret(client2.publicKey);
    // let secretClient2 = client2.calculateSharedSecret(client1.publicKey);
    // secretClient1 %= 256;
    // secretClient2 %= 256;
    // console.log("Secret Client 1 : ", secretClient1);
    // console.log("Secret Client 2 : ", secretClient2);

    // const messageEncrypted = this.encryptionDecryption.encrypt("Hello world", secretClient1);
    // const messageDecrypted = this.encryptionDecryption.decrypt(messageEncrypted, secretClient2);
    // console.log(messageEncrypted);
    // console.log(messageDecrypted);
  }

  onClickLogOut() {
    this.commandService.execute(this.authService.getLogOutCommand())
    .pipe(take(1))
    .subscribe(response => {
      if (response.status) {
        this.authService.logOut(); 
        this.isLoggedIn = this.authService.isLoggedIn();
        // this.router.navigateByUrl("log-in");
      }
    });
  }

  setCurrentOpenedNavItem() {
    if (this.router.url.includes('chat')) {
      this.currentOpenedNavItem = 'chat';
    }
    else if (this.router.url.includes('contact')) {
      this.currentOpenedNavItem = 'contact';
    }
    else if (this.router.url.includes('home')) {
      this.currentOpenedNavItem = 'home';
    }
    else {
      this.currentOpenedNavItem =  '';
    }
  }

  onClickNavItem(item: any) {
    this.currentOpenedNavItem = item;
    this.router.navigateByUrl(item);
  }

  onClickUserProfile() {
    const userId = this.userService.getCurrentUserId();
    this.router.navigateByUrl('/user/' + userId);
  }
}
