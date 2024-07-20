import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { Notification } from '../Models/Notification/Notification.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdateStatusReadNotification } from '../Models/Notification/UpdateStatusReadNotification.entity';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private apiUrl = "http://pitnik.somee.com";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<Notification>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Notification>>(`${this.apiUrl}/api/Notification/GetNotification`, { params,headers: this.userService.addHeaderToken()});
  }
  UpdateReadStatus(request:UpdateStatusReadNotification):Observable<BaseCommandResponse<Notification>>{
    return this.httpClient.post<BaseCommandResponse<Notification>>(`${this.apiUrl}/api/Notification/UpdateReadStatus`, request,{headers: this.userService.addHeaderToken()});
  }
}
