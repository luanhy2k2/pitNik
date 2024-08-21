export interface ReplyComment {
    id:number,
    commentId:number,
    commenterId:string,
    commenterName:string,
    responderId:string,
    responderName:string,
    responderImage:string,
    content:string,
    created:Date,
}
