export interface AddGroupMember{
    groupId:number,
    userId:number,
    status:GroupMemberStatus,
    joinedAt:Date,
    isCreate:boolean
}
export enum GroupMemberStatus{
    Pending,
    Accepted,
    Rejected
}