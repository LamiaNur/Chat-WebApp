import { Response } from "../models/response";

export class CommandResponse extends Response{
    name : string | undefined;
    metaData : any;
}