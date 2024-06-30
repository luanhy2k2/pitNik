import { Account } from "../Account/Account.entity";

export interface Notification{
    id:number,
    postId:number,
    isSeen:boolean,
    created:Date,
    sender:Account,
    receiverId:string,
    content:string
}