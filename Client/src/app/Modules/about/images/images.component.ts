import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { postImageUrl } from 'src/app/Environments/env';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css']
})
export class ImagesComponent {
  public postImageUrl = postImageUrl;
  constructor(private readonly UserService:UserService, private readonly Route:ActivatedRoute){}
  userId:string = "";
  Images:BaseQueriesResponse<string> = {
    pageIndex:1,
    pageSize:15,
    keyword:"",
    total:0,
    items:[]
  }
  ngOnInit(){
    this.Route.queryParams.subscribe(params =>{
      this.userId = params['id'] || this.UserService.getUser().id;
    })
    this.loadImages();
  }
  loadImages(){
    this.UserService.getImagesOfUser(this.userId,this.Images.pageIndex,this.Images.pageSize,this.Images.keyword).subscribe(res =>{
      this.Images = res;
    })
  }
}
