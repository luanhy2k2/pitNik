import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Account } from 'src/app/Models/Account/Account.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UserService } from 'src/app/services/User.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent {
  constructor(private readonly userService:UserService, 
    private readonly FriendService:FriendShipService,
    private route:ActivatedRoute){}
  userImage:string = "";
  nameUser:string = "";
  addressUser:string = "";
  userId:string = "";
  CreateFriend:CreateFriendShip = {
    receiverId: "",
    senderId: "",
    status: FriendshipStatus.Pending,
    requestedAt:new Date()
  }
  ngOnInit(){
    this.loadUserInfo();
  }
  loadUserInfo(){
    this.route.queryParams.subscribe(params => {
      this.userId = params['id'] || this.userService.getUser().id;
    });
    this.userService.getPersionalInfor(this.userId).subscribe(res =>{
      this.userImage = res.image;
      this.nameUser = res.name;
      this.addressUser = res.address;
    })
  }
  CreateFriendShip(){
    this.CreateFriend.receiverId = this.userId;
    this.FriendService.create(this.CreateFriend).subscribe(res =>{
      if(res.success == true){
        alert(res.message)
      }
    })
  }
}
