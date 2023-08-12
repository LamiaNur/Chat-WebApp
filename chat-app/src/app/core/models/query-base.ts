export class QueryBase{
    
    Offset: any = 0;
    limit: any = 1;
    fieldValues: any;
    apiUrl: string | undefined;
    
    getNextPaginationQuery() {
        this.Offset += this.limit;
        return this;
    }
}