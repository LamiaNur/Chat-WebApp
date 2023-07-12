import { EncrytptionDecryptionFactory } from "src/app/core/helpers/encryption-decryption-helper";

export class ChatProcessor{
    public static process(chat : any, sharedSecret: any) {
        const encryptionDecryptionHelper = EncrytptionDecryptionFactory.getEncryptionDecryption();
        const chatTime = new Date(chat.sentAt);
        const currentTime = new Date();
        if (chatTime.getDay() === currentTime.getDay()) {
          chat.sentAt = chatTime.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
        } else {
          chat.sentAt = chatTime.toLocaleDateString();
        }
        chat.message = encryptionDecryptionHelper.decrypt(chat.message, sharedSecret);
        return chat;
    }
}