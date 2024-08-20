import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { Notification } from '../Models/Notification/Notification.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdateStatusReadNotification } from '../Models/Notification/UpdateStatusReadNotification.entity';
import { apiUrl } from '../Environments/env';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private readonly httpClient: HttpClient) { }
  getPagedData(pageIndex:number, pageSize:number,keyword:string): Observable<BaseQueriesResponse<Notification>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Notification>>(`${apiUrl}/api/Notification/GetNotification`, { params});
  }
  UpdateReadStatus(request:UpdateStatusReadNotification):Observable<BaseCommandResponse<Notification>>{
    return this.httpClient.post<BaseCommandResponse<Notification>>(`${apiUrl}/api/Notification/UpdateReadStatus`, request);
  }
}
