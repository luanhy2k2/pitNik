export interface Conversation{
    id:number,
    member:UserConversation[],
    message:string,
    isSeen:boolean,
    timeMessage:string,
}
export interface UserConversation{
    id:string,
    name:string,
    image:string,
    isCurrentUser:boolean
}