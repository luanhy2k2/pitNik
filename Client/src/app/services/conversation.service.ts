import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { Conversation } from '../Models/Conversation/Conversation.entity';
import { CreateConversation } from '../Models/Conversation/CreateConversation.entity';
import { apiUrl } from '../Environments/env';

@Injectable({
  providedIn: 'root'
})
export class ConversationService {
  constructor(private readonly httpClient: HttpClient) { }
  getPagedData(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Conversation>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Conversation>>(`${apiUrl}/api/Conversation/GetAll`, { params});
  }
  getByFriendId(id:string): Observable<Conversation> {
    return this.httpClient.get<Conversation>(`${apiUrl}/api/Conversation/GetByFriendId/${id}`);
  }
  createConversation(request:CreateConversation): Observable<BaseCommandResponse<Conversation>> {
    return this.httpClient.post<BaseCommandResponse<Conversation>>(`${apiUrl}/api/Conversation/Create`, request);
  }
}
