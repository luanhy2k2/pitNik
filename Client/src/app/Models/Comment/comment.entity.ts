import { BaseQueriesResponse } from "../Common/BaseQueriesResponse.entity";

export interface Comment{
    userId:string,
    nameUser:string,
    content:string,
    postId:number,
    id:number,
    created:Date
}
export const defaultCommentQuery: BaseQueriesResponse<Comment> = {
    pageIndex: 1,
    pageSize: 10,
    total: 0,
    items: [],
    keyword: "",
};