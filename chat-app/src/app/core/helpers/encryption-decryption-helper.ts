export interface IEncryptionDecryption{
    encrypt(data: any, secretKey: any) : any;
    decrypt(data: any, secretKey: any) : any;
}

export class XorEncryptionDecryption implements IEncryptionDecryption{
    
    encrypt(data: any, secretKey: any) {
        let encryptedData = '';
        for (let i = 0; i < data.length; i++) {
            const charCode = data.charCodeAt(i);
            const encryptedCharCode = charCode ^ secretKey;
            encryptedData += String.fromCharCode(encryptedCharCode);
        }
        return encryptedData;
    }

    decrypt(data: any, secretKey: any) {
        let decryptedData = '';
        for (let i = 0; i < data.length; i++) {
            const charCode = data.charCodeAt(i);
            const decryptedCharCode = charCode ^ secretKey;
            decryptedData += String.fromCharCode(decryptedCharCode);
        }
        return decryptedData;
    }

}

export class EncrytptionDecryptionFactory{
    private static xorEncryptionDecryption = new XorEncryptionDecryption();
    public static getEncryptionDecryption(method : any = "xor") : IEncryptionDecryption{
        switch (method) {
            case "xor":
                return this.xorEncryptionDecryption;
            default:
                return this.xorEncryptionDecryption;
        }
    }
}