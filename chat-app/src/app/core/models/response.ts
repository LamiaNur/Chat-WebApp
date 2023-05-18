export class Response {
    public name : string | undefined;
    public message : string | undefined;
    public status : string | undefined;
    public metaData : any;
    
    public getData(key: string) {
        return this.metaData.key;
    }

    public setData(key: string, data: any) {
        this.metaData[key] = data;
    }
}