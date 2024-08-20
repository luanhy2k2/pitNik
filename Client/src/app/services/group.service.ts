import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { Group } from '../Models/Group/Group.entity';
import { GroupMember } from '../Models/Group/GroupMember.entity';
import { CreateGroup } from '../Models/Group/CreateGroup.entity';
import { Invitation, JoinGroup } from '../Models/Group/JoinGroup.entity';
import { GroupMemberStatus } from '../Models/Group/AddGroupMember.entity';
import { apiUrl } from '../Environments/env';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getMyGroup(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Group>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Group>>(`${apiUrl}/api/Group/GetMyGroup`, { params});
  }
  getGroup(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Group>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Group>>(`${apiUrl}/api/Group/GetGroup`);
  }
  getGroupDetail(groupId:number): Observable<Group> {
    return this.httpClient.get<Group>(`${apiUrl}/api/Group/GetGroup/${groupId}`);
  }
  getMemberOfGroup(groupId:number,pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<GroupMember>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('GroupId', groupId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<GroupMember>>(`${apiUrl}/api/Group/GetMemberOfGroup`);
  }
  create(group: CreateGroup): Observable<BaseCommandResponse<Group>> {
    const formData: FormData = new FormData();
    formData.append('Name', group.name);
    formData.append('Description', group.description);
    formData.append('BackGround', group.background, group.background.name);
    return this.httpClient.post<BaseCommandResponse<Group>>(`${apiUrl}/api/Group/CreateGroup`, formData);
  }
  JoinGroup(JoinGroup: JoinGroup): Observable<BaseCommandResponse<JoinGroup>> {
    return this.httpClient.post<BaseCommandResponse<JoinGroup>>(`${apiUrl}/api/Group/JoinGroup`, JoinGroup);
  }
  UpdateStatusInvitation(id:number, status:GroupMemberStatus): Observable<BaseCommandResponse<GroupMember>> {
    var invitation = {
      id: id,
      memberStatus:status
    }
    return this.httpClient.post<BaseCommandResponse<GroupMember>>(`${apiUrl}/api/Group/UpdateStatusInvitation`, invitation);
  }
  getInvitation(groupId:number,pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Invitation>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('GroupId', groupId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Invitation>>(`${apiUrl}/api/Group/GetInvitation`);
  }
}
