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
import { apiUrl } from '../Environments/env';
import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { MessageReadStatus } from '../Models/Message/MessageReadStatus';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  Messages: BaseQueriesResponse<Message> = {
    pageIndex: 1,
    pageSize: 20,
    keyword: "",
    items: [],
    total: 0
  };
  imageMessageSrcs: (string | ArrayBuffer | null)[] = [];
  CreateMessageRequest: CreateMessage = {
    conversationId: 0,
    content: "",
    // files: []
  }
  isHidden: boolean = true;
  private hubConnection: HubConnection;
  private mesageAddedSource = new Subject<Message>();
  messageAdded$ = this.mesageAddedSource.asObservable();
  constructor(private readonly httpClient: HttpClient, private readonly userService: UserService) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${apiUrl}/HubMessage`, {
        accessTokenFactory: () => {
          const user = this.userService.getUser();
          return user ? user.token : '';
        },
        transport: HttpTransportType.LongPolling,
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
  }
  startConnection() {
    this.hubConnection.start()
      .then(() => {
        console.log('Đã kết nối SignalR');
        this.registerSignalREvents();
      })
      .catch(err => {
        console.log('Lỗi khi kết nối: ', err)
      });
  }
  stopConnection() {
    if (this.hubConnection) {
      try {
        this.hubConnection.stop();
        console.log('Đã ngắt kết nối SignalR');
      } catch (err) {
        console.log('Lỗi khi dừng kết nối: ', err);
      }
    }
  }
  async joinRoom(roomName: string) {
    try {
      console.log("joined room:", roomName)
      return await this.hubConnection.invoke("Join", roomName);
    } catch (err) {
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  async LeaveRoom(roomName: string) {
    try {
      console.log("leaved room success:", roomName)
      return await this.hubConnection.invoke("Leave", roomName);
    } catch (err) {
      console.error('Error while leaving room:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  private registerSignalREvents() {
    this.hubConnection.on('newMessage', (message) => {
      console.log("message:", message)
      this.mesageAddedSource.next(message);
    });
  }
  async sendMessage(messsage: CreateMessage) {
    return this.hubConnection.invoke('SendMessage', messsage)
      .catch(error => console.log(error));
  }
  async UpdateMessageReadStatus(readStatus: MessageReadStatus) {
    return this.hubConnection.invoke('UpdateMessageReadStatus', readStatus)
      .catch(error => console.log(error));
  }
  getPagedData(pageIndex: number, pageSize: number, conversationId: number, keyword: string): Observable<BaseQueriesResponse<Message>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('ConversionId', conversationId.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Message>>(`${apiUrl}/api/Message/Get`, { params });
  }
  UploadFile(Files: File[]): Observable<BaseCommandResponse<string>> {
    const formData: FormData = new FormData();
    Files.forEach(file => formData.append('Files', file, file.name));
    return this.httpClient.post<BaseCommandResponse<string>>(`${apiUrl}/api/Message/UploadFile`, formData);
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
