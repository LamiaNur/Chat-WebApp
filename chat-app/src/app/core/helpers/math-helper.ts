export class MathHelper {
    
    public static getBigMod(base : any, pow : any, mod: any) {
        let res = 1;
        while (pow !== 0) {
            if (pow % 2 !== 0) {
                res = res * base % mod;
                pow--;
            } else {
                base = base * base % mod;
                pow /= 2;
            }
        }
        return res;
    }

    public static getPrimeFactors(num : any, distinct : boolean = false) {
        let factors = [];
        if (num % 2 === 0) {
            if (distinct) factors.push(2);
            while (num % 2 === 0) {
                if (!distinct) factors.push(2);
                num /= 2;
            }
        }
        for (let i = 3; i * i <= num; i += 2) {
            if (num % i === 0) {
                if (distinct) factors.push(i);
                while (num % i === 0) {
                    if (!distinct) factors.push(i);
                    num /= i;
                }
            }
        }
        if (num > 1) factors.push(num);
        return factors;
    }

    public static isPrimeNumber(num : any) {
        if (num == 1) return false;
        if (num == 2) return true;
        if (num % 2 == 0) return false;
        let iter = 0;
        for (let i = 3; i * i <= num; i += 2) {
            if (num % i === 0) return false;
            iter++;
        }
        console.log("Check prime iteration Count : ", iter);
        return true;
    }

    public static getRandomPrimitiveRootOfPrime(primeNumber : any, maxIter : any = 100) {
        const phi = primeNumber - 1;
        const factors = this.getPrimeFactors(phi, true);
        for (let iter = 0; iter < maxIter;  iter++) {
            const res = this.getRandomNumber(2, primeNumber - 1);
            let found = true;
            for (let factor of factors) {
                if (!found) break;
                found &&= (this.getBigMod(res, phi / factor, primeNumber) !== 1); 
            }
            if (found) {
                console.log("Primitive root iteration Count : ",iter);
                return res;
            }
        }
        return -1;
    }

    public static generateRandomPrime(minRange : any, maxRange: any) {
        let prime = 1;
        let iter = 0;
        while (!this.isPrimeNumber(prime)) {
            prime = this.getRandomNumber(minRange, maxRange);
            iter++;
        }
        console.log("Found Prime Iteration Count : ", iter);
        return prime;
    }

    public static getRandomNumber(minRange: any, maxRange: any) {
        return Math.floor(Math.random() * (maxRange - minRange + 1)) + minRange;
    }
}