import { Component } from '@angular/core';
import { Account, Gender, GeneralInfo } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { MyFriend } from 'src/app/Models/FriendShip/Myfriend.entity';
import { UserService } from 'src/app/services/User.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent {
  constructor(private readonly UserService:UserService, private readonly FriendService:FriendShipService){}
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
  ngOnInit(){
    this.LoadGeneralInfo();
    this.LoadPersionalInfo();
    this,this.LoadMyFriends();
  }
  LoadGeneralInfo(){
    var userId = this.UserService.getUser().id;
    this.UserService.getGeneralInfor(userId).subscribe(res =>{
      this.GeneralInfo = res;
    })
  }
  LoadPersionalInfo(){
    var userId = this.UserService.getUser().id;
    this.UserService.getPersionalInfor(userId).subscribe(res =>{
      this.PersionalInfo = res;
    })
  }
  LoadMyFriends(){
    this.FriendService.GetMyFriend(this.MyFriends.pageIndex,this.MyFriends.pageSize,this.MyFriends.keyword).subscribe(res =>{
      this.MyFriends.items = res.items,
      this.MyFriends.total = res.total
    })
  }
}
