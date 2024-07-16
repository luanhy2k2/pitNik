import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasePaging } from '../Models/Paging.entity';
import { UserService } from './User.service';
import { CreatePost } from '../Models/Post/CreatePost.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { Post } from '../Models/Post/Post.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  Search(pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${this.apiUrl}/api/Post/Search`, { params,headers: this.userService.addHeaderToken()});
  }
  getPost(pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${this.apiUrl}/api/Post/GetPost`, { params,headers: this.userService.addHeaderToken()});
  }
  getPostOfGroup(groupId:number,pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
    .set('GroupId', groupId.toString())
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${this.apiUrl}/api/Post/GetPostOfGroup`, { params,headers: this.userService.addHeaderToken()});
  }
  createPost(post: CreatePost): Observable<BaseCommandResponse> {
    const formData: FormData = new FormData();
    formData.append('Id', post.id.toString());
    formData.append('Created', post.created.toISOString());
    formData.append('UserId', this.userService.getUser().id);
    formData.append('Content', post.content);
    if(post.groupId != 0)
      formData.append('GroupId',post.groupId.toString())
    post.files.forEach(file => formData.append('Files', file, file.name));

    return this.httpClient.post<BaseCommandResponse>(`${this.apiUrl}/api/Post/Create`, formData,{headers: this.userService.addHeaderToken()});
  }
  getById(postId:number): Observable<Post> {
    return this.httpClient.get<Post>(`${this.apiUrl}/api/Post/GetById/${postId}`, {headers: this.userService.addHeaderToken()});
  }
}
