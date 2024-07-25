import { BaseQueriesResponse } from "../Common/BaseQueriesResponse.entity";
import { ReplyComment } from "./reply-comment";

export interface Comment{
    userId:string,
    nameUser:string,
    content:string,
    postId:number,
    id:number,
    created:Date,
    imageUser:string,
    totalReply:number,
    PageIndexReplyComment:number,
    Reply:ReplyComment[],
    totalPageReply:number
}
export const defaultCommentQuery: BaseQueriesResponse<Comment> = {
    pageIndex: 1,
    pageSize: 10,
    total: 0,
    items: [],
    keyword: "",
};