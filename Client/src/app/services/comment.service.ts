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

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = "http://pitnik.somee.com";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pageIndex:number, pageSize:number,postId:number): Observable<BaseQueriesResponse<Comment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PostId', postId.toString())
      .set('PageSize', pageSize.toString());
    
    return this.httpClient.get<BaseQueriesResponse<Comment>>(`${this.apiUrl}/api/Comment/GetComment`, { params,headers: this.userService.addHeaderToken()});
  }
  create(comment: CreateComment): Observable<any> {
    comment.userId = this.userService.getUser().id;
    return this.httpClient.post(`${this.apiUrl}/api/Comment/Create`, comment,{headers: this.userService.addHeaderToken()});
  }
}
