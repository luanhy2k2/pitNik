import { GroupMemberStatus } from "./AddGroupMember.entity";

export interface JoinGroup{
    groupId:number,
    userId:string,
    groupMemberStatus:GroupMemberStatus
}
export interface Invitation{
    id:number,
    userId:string,
    name:string,
    address:string,
    image:string,
    aboutMe:string,
    requestAt:string,
    memberStatus:GroupMemberStatus
}