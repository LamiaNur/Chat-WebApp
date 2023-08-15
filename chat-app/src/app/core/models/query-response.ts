import { Response } from "../models/response";

export class QueryResponse extends Response{
    name: string | undefined;
    Offset: number | undefined;
    limit: number | undefined;
    totalCount: number | undefined;
    items : any;
    metaData: any;
}
