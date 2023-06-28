import { MathHelper } from "../helpers/math-helper";

export class DiffieHellmanKeyExchange {
    
    prime : any;
    alpha : any;
    privateKey: any;
    publicKey: any;
    sharedSecred: any;

    constructor() {
        this.prime = MathHelper.generateRandomPrime(1000000, 9999999);
        this.alpha = MathHelper.getRandomPrimitiveRootOfPrime(this.prime);
    }

    initialize(prime: any, alpha: any) {
        this.prime = prime;
        this.alpha = alpha;
    }

    generatePrivateKey() {
        this.privateKey = MathHelper.getRandomNumber(1, 1000000); // it should always be in range [1, prime]
        return this.privateKey;
    }

    calculatePublicKey() {
        this.publicKey = MathHelper.getBigMod(this.alpha, this.privateKey, this.prime);
        return this.publicKey;
    }

    calculateSharedSecret(receivedPublicKey: any) {
        this.sharedSecred = MathHelper.getBigMod(receivedPublicKey, this.privateKey, this.prime);
        return this.sharedSecred;
    }
}