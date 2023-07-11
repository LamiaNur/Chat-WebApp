import { Injectable } from "@angular/core";
import { DiffieHellmanKeyExchange } from "../cryptography/diffie-hellman-key-exchange";

@Injectable({
    providedIn: 'root',
})
export class SecurtiyService {
    prime : any = 1000000007;
    alpha: any = 37;
    diffie: DiffieHellmanKeyExchange = new DiffieHellmanKeyExchange();

    createAndSavePrivateKey(token: any) {
        let hash_val = 0;
        let p = 31;
        let curP = 1;
        for (let i = 0; i < token.length; i++) {
            hash_val += token[i] * curP % this.prime;
            curP *= p;
            curP %= this.prime;
            hash_val %= this.prime;
        }
        localStorage.setItem("PrivateKey", hash_val.toString());
        return hash_val;
    }

    getPrivateKey() { // current user private key
        return localStorage.getItem("PrivateKey");
    }

    getPublicKey(privateKey : any) { // current user public key
        this.diffie.initialize(this.prime, this.alpha);
        return this.diffie.calculatePublicKey(privateKey);
    }

    getSharedSecretKey(publicKey: any) {
        const privateKey = this.getPrivateKey();
        return this.diffie.calculateSharedSecret(publicKey, privateKey);
    }

    getPublicKeyByUserId(userId: any) { // get the public key by user id through api
        
    }

}