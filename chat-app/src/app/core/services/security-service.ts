import { Injectable } from "@angular/core";
import { DiffieHellmanKeyExchange } from "../cryptography/diffie-hellman-key-exchange";

@Injectable({
    providedIn: 'root',
})
export class SecurtiyService {
    prime : any = 7227973;
    alpha: any = 1738263;
    diffie: DiffieHellmanKeyExchange = new DiffieHellmanKeyExchange();
    
    constructor() {
        this.diffie.initialize(this.prime, this.alpha);
    }
    
    createAndSavePrivateKey(token: any) {
        let hash_val = 0;
        let p = 31;
        let curP = 1;
        for (let i = 0; i < token.length; i++) {
            hash_val += token[i] * curP % 1738263;
            curP *= p;
            curP %= 1738263;
            hash_val %= 1738263;
        }
        localStorage.setItem("PrivateKey", hash_val.toString());
        console.log(hash_val);
        return hash_val;
    }

    getPrivateKey() { // current user private key
        return localStorage.getItem("PrivateKey");
    }

    getPublicKey(privateKey : any) { // current user public key
        return this.diffie.calculatePublicKey(privateKey);
    }

    getSharedSecretKey(publicKey: any, privateKey: any = '') {
        if (privateKey === '') privateKey = this.getPrivateKey();
        return this.diffie.calculateSharedSecret(publicKey, privateKey);
    }

    getPublicKeyByUserId(userId: any) { // get the public key by user id through api
        
    }

}