import { FriendshipStatus } from "./FriendShip.entity";

export interface UpdateStatusFriend{
    id:number,
    status: FriendshipStatus,
    requestedAt:Date
}