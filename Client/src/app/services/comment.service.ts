import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number,postId:number): Observable<BaseQueriesResponse<Comment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PostId', postId.toString())
      .set('PageSize', pageSize.toString());
    
    return this.httpClient.get<BaseQueriesResponse<Comment>>(`${this.apiUrl}/api/Comment/GetComment`, { params,headers: this.userService.addHeaderToken()});
  }
  getReplyComment(pageIndex:number, CommentId:number): Observable<BaseQueriesResponse<ReplyComment>> {
    return this.httpClient.get<BaseQueriesResponse<ReplyComment>>(`${this.apiUrl}/api/Comment/GetReplyComment/${CommentId}/${pageIndex}`, {headers: this.userService.addHeaderToken()});
  }
  create(comment: CreateComment): Observable<BaseCommandResponse<Comment>> {
    comment.userId = this.userService.getUser().id;
    return this.httpClient.post<BaseCommandResponse<Comment>>(`${this.apiUrl}/api/Comment/Create`, comment,{headers: this.userService.addHeaderToken()});
  }
  createReplyComment(reply: CreateReplyComment): Observable<BaseCommandResponse<ReplyComment>> {
    return this.httpClient.post<BaseCommandResponse<ReplyComment>>(`${this.apiUrl}/api/Comment/CreateReply`, reply,{headers: this.userService.addHeaderToken()});
  }
}
