import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasePaging } from '../Models/Paging.entity';
import { UserService } from './User.service';
import { CreatePost } from '../Models/Post/CreatePost.entity';
import { BaseQueriesResponse } from '../Models/Common/BaseQueriesResponse.entity';
import { Post } from '../Models/Post/Post.entity';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { UpdatePost } from '../Models/Post/update-post';
import { apiUrl } from '../Environments/env';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  Search(pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${apiUrl}/api/Post/Search`, { params});
  }
  getPost(pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${apiUrl}/api/Post/GetPost`, {params});
  }
  getPostOfGroup(groupId:number,pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
    .set('GroupId', groupId.toString())
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${apiUrl}/api/Post/GetPostOfGroup`, { params});
  }
  getPostOfUser(userId:string,pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Post>> {
    let params = new HttpParams()
      .set('UserId', userId.toString())
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());
    if (keyword) {
      params = params.set('Keyword', keyword);
    }
    return this.httpClient.get<BaseQueriesResponse<Post>>(`${apiUrl}/api/Post/GetPostOfUser`, { params});
  }
  createPost(post: CreatePost): Observable<BaseCommandResponse<Post>> {
    const formData: FormData = new FormData();
    formData.append('Id', post.id.toString());
    formData.append('Created', post.created.toISOString());
    formData.append('UserId', this.userService.getUser().id);
    formData.append('Content', post.content);
    if(post.groupId != 0)
      formData.append('GroupId',post.groupId.toString())
    post.files.forEach(file => formData.append('Files', file, file.name));

    return this.httpClient.post<BaseCommandResponse<Post>>(`${apiUrl}/api/Post/Create`, formData);
  }
  updatePost(post: UpdatePost): Observable<BaseCommandResponse<Post>> {
    const formData: FormData = new FormData();
    formData.append('Id', post.id.toString());
    formData.append('Content', post.content);
    post.files.forEach(file => formData.append('Files', file, file.name));
    post.imageNameDelete.forEach(image => formData.append('ImageNameDelete', image));
    return this.httpClient.post<BaseCommandResponse<Post>>(`${apiUrl}/api/Post/Update`, formData);
  }
  getById(postId:number): Observable<Post> {
    return this.httpClient.get<Post>(`${apiUrl}/api/Post/GetById/${postId}`);
  }
}
