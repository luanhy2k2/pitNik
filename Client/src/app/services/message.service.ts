import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { Message } from '../Models/Message/Message.entity';
import { CreateMessage } from '../Models/Message/CreateMessage.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number,conversationId:number, keyword:string): Observable<BaseQueriesResponse<Message>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('ConversionId', conversationId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Message>>(`${this.apiUrl}/api/Message/Get`, { params,headers: this.userService.addHeaderToken()});
  }
  create(message: CreateMessage): Observable<BaseCommandResponse> {
    const formData: FormData = new FormData();
    formData.append('ConversationId', message.conversationId.toString());
    formData.append('Content', message.content);
    
    message.files.forEach(file => formData.append('Files', file, file.name));
    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Message/Create`, formData,{headers: this.userService.addHeaderToken()});
  }
}
