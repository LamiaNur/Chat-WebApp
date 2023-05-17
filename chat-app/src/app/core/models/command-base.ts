export class CommandBase {
    fieldValues : any;
    apiUrl: string | undefined;
    
    setValue(key : string, value : any) : void {
        this.fieldValues[key] = value;
    }
    
    getValue(key: string) : any {
        return this.fieldValues[key];
    }
}