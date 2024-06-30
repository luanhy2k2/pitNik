export interface Conversation{
    id:number,
    user1:UserConversation,
    user2:UserConversation,
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