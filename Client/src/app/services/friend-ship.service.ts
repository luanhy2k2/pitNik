import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { FriendShip } from '../Models/FriendShip/FriendShip.entity';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdateStatusFriend } from '../Models/FriendShip/UpdateStatusFriend.entity';
import { MyFriend } from '../Models/FriendShip/Myfriend.entity';

@Injectable({
  providedIn: 'root'
})
export class FriendShipService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<FriendShip>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<FriendShip>>(`${this.apiUrl}/api/FriendShip/Get`, { params,headers: this.userService.addHeaderToken()});
  }
  create(FriendShip: CreateFriendShip): Observable<BaseCommandResponse<CreateFriendShip>> {
    return this.httpClient.post<BaseCommandResponse<CreateFriendShip>>(`${this.apiUrl}/api/FriendShip/Create`, FriendShip,{headers: this.userService.addHeaderToken()});
  }
  Update(FriendShip: UpdateStatusFriend): Observable<BaseCommandResponse<CreateFriendShip>> {
    return this.httpClient.post<BaseCommandResponse<CreateFriendShip>>(`${this.apiUrl}/api/FriendShip/Update`, FriendShip,{headers: this.userService.addHeaderToken()});
  }
  GetMyFriend(UserId:string, pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<MyFriend>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString())
      .set('CurrentUserId', UserId);
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<MyFriend>>(`${this.apiUrl}/api/FriendShip/GetMyFriend`, { params,headers: this.userService.addHeaderToken()});
  }
}
