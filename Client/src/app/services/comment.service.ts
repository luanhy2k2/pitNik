import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BasePaging } from '../Models/Paging.entity';
import { CreatePost } from '../Models/Post/CreatePost.entity';
import { UserService } from './User.service';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { Comment } from '../Models/Comment/comment.entity';
import { commentRequest } from '../Models/Comment/commentRequest.entity';
import { ReplyComment } from '../Models/Comment/reply-comment';
import { CreateReplyComment } from '../Models/Comment/create-reply-comment';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { apiUrl } from '../Environments/env';
import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private hubConnection: HubConnection;
  private commentAddedSource = new Subject<Comment>();
  private replyCommentAddedSource = new Subject<ReplyComment>();
  commentAdded$ = this.commentAddedSource.asObservable();
  replyCommentAdded$ = this.replyCommentAddedSource.asObservable();
  constructor(private readonly httpClient: HttpClient, private readonly userService: UserService) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${apiUrl}/HubInteraction`, {
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
        console.log('Đã kết nối Hub Commnet');
        this.registerSignalREvents();
      })
      .catch(err => {
        console.log('Lỗi khi kết nối Hub Comment: ', err)
      });
  }
  stopConnection() {
    if (this.hubConnection) {
      try {
        this.hubConnection.stop();
        console.log('Đã ngắt kết nối Hub Comment');
      } catch (err) {
        console.log('Lỗi khi dừng kết nối Hib Comment: ', err);
      }
    }
  }
  async joinPost(postId: string) {
    try {
      console.log("joined post:", postId)
      return await this.hubConnection.invoke("Join", postId);
    } catch (err) {
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  async LeavePost(postId: string) {
    try {
      console.log("leaved post success:", postId)
      return await this.hubConnection.invoke("Leave", postId);
    } catch (err) {
      console.error('Error while leaving post:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  private registerSignalREvents() {
    this.hubConnection.on('addComment', (comment) => {
      console.log("Comment:", comment)
      this.commentAddedSource.next(comment);
    });
  }
  getPagedData(pageIndex:number, pageSize:number,postId:number): Observable<BaseQueriesResponse<Comment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PostId', postId.toString())
      .set('PageSize', pageSize.toString());
    
    return this.httpClient.get<BaseQueriesResponse<Comment>>(`${apiUrl}/api/Comment/GetComment`, { params});
  }
  getReplyComment(pageIndex:number, CommentId:number): Observable<BaseQueriesResponse<ReplyComment>> {
    return this.httpClient.get<BaseQueriesResponse<ReplyComment>>(`${apiUrl}/api/Comment/GetReplyComment/${CommentId}/${pageIndex}`);
  }
  async sendComment(Comment: CreateComment) {
    Comment.userId = this.userService.getUser().id;
    return this.hubConnection.invoke('Comment', Comment)
      .catch(error => console.log(error));
  }
  async sendReplyComment(Comment: CreateReplyComment) {
    return this.hubConnection.invoke('ReplyComment', Comment)
      .catch(error => console.log(error));
  }
  create(comment: CreateComment): Observable<BaseCommandResponse<Comment>> {
    comment.userId = this.userService.getUser().id;
    return this.httpClient.post<BaseCommandResponse<Comment>>(`${apiUrl}/api/Comment/Create`, comment);
  }
  createReplyComment(reply: CreateReplyComment): Observable<BaseCommandResponse<ReplyComment>> {
    return this.httpClient.post<BaseCommandResponse<ReplyComment>>(`${apiUrl}/api/Comment/CreateReply`, reply);
  }
}
