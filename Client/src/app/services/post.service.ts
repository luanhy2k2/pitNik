import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasePaging } from '../Models/Paging.entity';
import { UserService } from './User.service';
import { CreatePost } from '../Models/Post/CreatePost.entity';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly httpClient: HttpClient,private readonly userService:UserService) { }
  getPagedData(pagingDto: BasePaging): Observable<any> {
    let params = new HttpParams()
      .set('PageIndex', pagingDto.pageIndex.toString())
      .set('PageSize', pagingDto.pageSize.toString());
    if (pagingDto.keyword) {
      params = params.set('Keyword', pagingDto.keyword);
    }
    return this.httpClient.get<any>(`${this.apiUrl}/api/Post/GetAll`, { params,headers: this.userService.addHeaderToken()});
  }
  createPost(post: CreatePost): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('Id', post.id.toString());
    formData.append('Created', post.created.toISOString());
    formData.append('UserId', post.userId);
    formData.append('Content', post.content);
    
    post.files.forEach(file => formData.append('Files', file, file.name));

    return this.httpClient.post(`${this.apiUrl}/api/Post/Create`, formData,{headers: this.userService.addHeaderToken()});
  }
}
