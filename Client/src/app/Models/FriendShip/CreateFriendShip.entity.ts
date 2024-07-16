import { FriendshipStatus } from "./FriendShip.entity";

export interface CreateFriendShip{
    senderId:string,
    receiverId:string,
    status:FriendshipStatus,
    requestedAt:Date
}