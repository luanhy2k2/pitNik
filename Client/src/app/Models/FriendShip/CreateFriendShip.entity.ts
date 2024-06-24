import { FriendshipStatus } from "./FriendShip.entity";

export interface CreateFriendShip{
    senderUserName:string,
    receiverId:string,
    status:FriendshipStatus,
    requestedAt:Date
}