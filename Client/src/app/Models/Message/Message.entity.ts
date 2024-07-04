export interface Message{
    id:number
    sender:UserMessage,
    conversationId:number,
    content:string,
    isSentByCurrentUser:boolean,
    created:string
}
export interface UserMessage{
    id:string,
    name:string,
    image:string
}