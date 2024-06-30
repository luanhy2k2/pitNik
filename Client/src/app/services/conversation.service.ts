import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { CreateMessage } from '../Models/Message/CreateMessage.entity';
import { Message } from '../Models/Message/Message.entity';
import { UserService } from './User.service';
import { Conversation } from '../Models/Conversation/Conversation.entity';

@Injectable({
  providedIn: 'root'
})
export class ConversationService {
  private apiUrl = "https://localhost:7261";
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
  create(message: CreateMessage): Observable<BaseCommandResponse> {
    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Message/Create`, message,{headers: this.userService.addHeaderToken()});
  }
}
