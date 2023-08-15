export class QueryBase{

    Offset: any = 0;
    limit: any = 1;
    metaData: any;
    apiUrl: string | undefined;

    getNextPaginationQuery() {
        this.Offset += this.limit;
        return this;
    }
}
