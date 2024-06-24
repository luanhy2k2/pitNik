export interface FriendShip{
    id:number,
    senderId:string,
    senderName:string,
    senderImage:string,
    receiverId:string,
    status:FriendshipStatus,
    requestedAt:Date
}
export enum FriendshipStatus
{
    Pending,
    Accepted,
    Rejected
}