import { Comment } from "../Comment/comment.entity";
import { BaseQueriesResponse } from "../Common/BaseQueriesResponse.entity";

export interface Post{
    userId:string,
    imageUser:string,
    nameUser:string,
    content:string,
    totalReactions:number,
    totalComment:number,
    isReact:boolean,
    image:string[],
    id:number,
    created:Date,
    comment:Comment[],
    pageIndexComment:number,
    pageSizeComment:number
}