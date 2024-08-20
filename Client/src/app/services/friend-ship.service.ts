import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { FriendShip } from '../Models/FriendShip/FriendShip.entity';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdateStatusFriend } from '../Models/FriendShip/UpdateStatusFriend.entity';
import { MyFriend } from '../Models/FriendShip/Myfriend.entity';
import { apiUrl } from '../Environments/env';

@Injectable({
  providedIn: 'root'
})
export class FriendShipService {
  userId:string = "";
  constructor(private readonly httpClient: HttpClient) { }
  getPagedData(pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<FriendShip>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<FriendShip>>(`${apiUrl}/api/FriendShip/Get`, { params});
  }
  create(FriendShip: CreateFriendShip): Observable<BaseCommandResponse<CreateFriendShip>> {
    return this.httpClient.post<BaseCommandResponse<CreateFriendShip>>(`${apiUrl}/api/FriendShip/Create`, FriendShip);
  }
  Update(FriendShip: UpdateStatusFriend): Observable<BaseCommandResponse<CreateFriendShip>> {
    return this.httpClient.post<BaseCommandResponse<CreateFriendShip>>(`${apiUrl}/api/FriendShip/Update`, FriendShip);
  }
  GetFriendOfUser(pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<MyFriend>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString())
      .set('CurrentUserId', this.userId);
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<MyFriend>>(`${apiUrl}/api/FriendShip/GetMyFriend`, {params});
  }
}
