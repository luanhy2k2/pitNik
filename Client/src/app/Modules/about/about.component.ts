import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Account, Gender, GeneralInfo } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { MyFriend } from 'src/app/Models/FriendShip/Myfriend.entity';
import { HeaderComponent } from 'src/app/Partial/header/header.component';
import { UserService } from 'src/app/services/User.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent {
  constructor(private readonly UserService:UserService, 
     private readonly FriendService:FriendShipService, 
     private readonly FriendShipService:FriendShipService,
     private readonly route:ActivatedRoute
    ){}
  GeneralInfo:GeneralInfo = {
    id: 0,
    aboutMe:"",
    workAndExperience:"",
    hobbies:"",
    userId:"",
    education:""
  }
  PersionalInfo:Account = {
    id:"",
    name:"",
    userName:"",
    image:"",
    address:"",
    phoneNumber:"",
    email:"",
    birthday:new Date,
    gender:Gender.Male
  }
  MyFriends:BaseQueriesResponse<MyFriend> = {
    pageIndex: 1,
    pageSize: 10,
    items:[],
    total:0,
    keyword:""
  }
  userId:string = "";
  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      this.userId = params['id'] || this.UserService.getUser().id;
      this.FriendService.userId = this.userId
    });
    
    this.LoadGeneralInfo(this.userId);
    this.LoadPersionalInfo(this.userId);
    this.LoadMyFriends();
  }
  LoadGeneralInfo(userId:string){
    this.UserService.getGeneralInfor(userId).subscribe(res =>{
      this.GeneralInfo = res;
    })
  } 
  LoadPersionalInfo(userId:string){
    this.UserService.getPersionalInfor(userId).subscribe(res =>{
      this.PersionalInfo = res;
    })
  }
  LoadMyFriends(){
    this.FriendService.GetFriendOfUser( this.MyFriends.pageIndex,this.MyFriends.pageSize,this.MyFriends.keyword).subscribe(res =>{
      this.MyFriends.items = res.items,
      this.MyFriends.total = res.total
    })
  }
  
}
