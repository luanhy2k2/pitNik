import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { CreateMessage } from '../Models/Message/CreateMessage.entity';
import { Message } from '../Models/Message/Message.entity';
import { UserService } from './User.service';
import { Conversation } from '../Models/Conversation/Conversation.entity';
import { CreateConversation } from '../Models/Conversation/CreateConversation.entity';

@Injectable({
  providedIn: 'root'
})
export class ConversationService {
  private apiUrl = "http://pitnik.somee.com";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number, keyword:string): Observable<BaseQueriesResponse<Conversation>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Conversation>>(`${this.apiUrl}/api/Conversation/GetAll`, { params,headers: this.userService.addHeaderToken()});
  }
  getByFriendId(id:string): Observable<Conversation> {
    return this.httpClient.get<Conversation>(`${this.apiUrl}/api/Conversation/GetByFriendId/${id}`,{headers: this.userService.addHeaderToken()});
  }
  createConversation(request:CreateConversation): Observable<BaseCommandResponse<Conversation>> {
    return this.httpClient.post<BaseCommandResponse<Conversation>>(`${this.apiUrl}/api/Conversation/Create`, request,{headers: this.userService.addHeaderToken()});
  }
}
