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

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getMyGroup(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Group>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Group>>(`${this.apiUrl}/api/Group/GetMyGroup`, { params,headers: this.userService.addHeaderToken()});
  }
  getGroup(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Group>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Group>>(`${this.apiUrl}/api/Group/GetGroup`, { params,headers: this.userService.addHeaderToken()});
  }
  getGroupDetail(groupId:number): Observable<Group> {
    return this.httpClient.get<Group>(`${this.apiUrl}/api/Group/GetGroup/${groupId}`, {headers: this.userService.addHeaderToken()});
  }
  getMemberOfGroup(groupId:number,pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<GroupMember>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('GroupId', groupId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<GroupMember>>(`${this.apiUrl}/api/Group/GetMemberOfGroup`, { params,headers: this.userService.addHeaderToken()});
  }
  create(group: CreateGroup): Observable<BaseCommandResponse> {
    const formData: FormData = new FormData();
    formData.append('Name', group.name);
    formData.append('Description', group.description);
    formData.append('BackGround', group.background, group.background.name);
    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Group/CreateGroup`, formData,{headers: this.userService.addHeaderToken()});
  }
  JoinGroup(JoinGroup: JoinGroup): Observable<BaseCommandResponse> {
    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Group/JoinGroup`, JoinGroup,{headers: this.userService.addHeaderToken()});
  }
  UpdateStatusInvitation(id:number, status:GroupMemberStatus): Observable<BaseCommandResponse> {
    var invitation = {
      id: id,
      memberStatus:status
    }
    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Group/UpdateStatusInvitation`, invitation,{headers: this.userService.addHeaderToken()});
  }
  getInvitation(groupId:number,pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Invitation>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('GroupId', groupId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Invitation>>(`${this.apiUrl}/api/Group/GetInvitation`, { params,headers: this.userService.addHeaderToken()});
  }
}
