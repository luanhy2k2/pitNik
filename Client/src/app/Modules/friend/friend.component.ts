import { Component } from '@angular/core';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { MyFriend } from 'src/app/Models/FriendShip/Myfriend.entity';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.scss']
})
export class FriendComponent {
  constructor(private readonly friendService:FriendShipService){}
  MyFriends:BaseQueriesResponse<MyFriend> = {
    pageIndex: 1,
    pageSize: 10,
    items:[],
    total:0,
    keyword:""
  }
  ngOnInit(){
    this.LoadMyFriends();
  }
  LoadMyFriends(){
    this.friendService.GetMyFriend(this.MyFriends.pageIndex,this.MyFriends.pageSize,this.MyFriends.keyword).subscribe(res =>{
      this.MyFriends.items = res.items,
      this.MyFriends.total = res.total
    })
  }
}
