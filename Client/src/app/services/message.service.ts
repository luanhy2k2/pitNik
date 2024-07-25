import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { UserService } from './User.service';
import { Message } from '../Models/Message/Message.entity';
import { CreateMessage } from '../Models/Message/CreateMessage.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdateMessageReadStatus } from '../Models/Message/UpdateMessageReadStatus.entuty';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private apiUrl = "https://localhost:7261";
  Messages:BaseQueriesResponse<Message> = {
    pageIndex:1,
    pageSize:20,
    keyword:"",
    items:[],
    total:0
  };
  imageMessageSrcs: (string | ArrayBuffer | null)[] = [];
  CreateMessageRequest:CreateMessage = {
    conversationId:0,
    content:String.fromCodePoint(0x1F60A),
    files:[]
  }
  isHidden: boolean = true;
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
  create(message: CreateMessage): Observable<BaseCommandResponse<Message>> {
    const formData: FormData = new FormData();
    formData.append('ConversationId', message.conversationId.toString());
    formData.append('Content', message.content);
    
    message.files.forEach(file => formData.append('Files', file, file.name));
    return this.httpClient.post<BaseCommandResponse<Message>>(`${this.apiUrl}/api/Message/Create`, formData,{headers: this.userService.addHeaderToken()});
  }
  updateMessageReadStatus(request: UpdateMessageReadStatus): Observable<BaseCommandResponse<Message>> {
    return this.httpClient.post<BaseCommandResponse<Message>>(`${this.apiUrl}/api/Message/UpdateStatusRead/${request.conversationId}/${request.status}`, {},{headers: this.userService.addHeaderToken()});
  }
  LoadMessageConverstion(conversionId: number) {
    this.isHidden = !this.isHidden;
    this.CreateMessageRequest.conversationId = conversionId;
    this.getPagedData(this.Messages.pageIndex, this.Messages.pageSize, conversionId, this.Messages.keyword).subscribe(
      res => {
        res.items.forEach(item => {
          item.isSentByCurrentUser = item.sender.id == this.userService.getUser().id;
          this.Messages.items.push(item);
        });
      }
    );
  }
  private ConversationSource = new BehaviorSubject<string>("");
  loadConversation$ = this.ConversationSource.asObservable();
  toggleModal(userId: string) {
    this.ConversationSource.next(userId);
  }

}
