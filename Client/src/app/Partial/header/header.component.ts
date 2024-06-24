import { Component } from '@angular/core';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendShip } from 'src/app/Models/FriendShip/FriendShip.entity';
import { ChatHubService } from 'src/app/services/chatHub.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(private readonly FriendService:FriendShipService, private readonly HubService:ChatHubService){}
  isHidden: boolean = true;
  FriendPending:BaseQueriesResponse<FriendShip> = {
    pageIndex: 1,
    pageSize: 10,
    items:[],
    total:0,
    keyword:""
  }
  LoadFrienPending(){
    this.FriendService.getPagedData(this.FriendPending.pageIndex,this.FriendPending.pageSize,this.FriendPending.keyword).subscribe(
      res =>{
        this.FriendPending.items = res.items,
        this.FriendPending.total = res.total
      },
      err =>{
        alert("Đã có lỗi xảy ra!")
      }
    )
  }
  ngOnInit(){
    this.LoadFrienPending();
    this.HubService.addFriendPendingLister((FriendPending: CreateFriendShip) => {
      this.LoadFrienPending();
      console.log(FriendPending)
    });
  }
  chatMessages:any;
  message:any
  toggleDiv() {
    this.isHidden = !this.isHidden;
  }
  sendNewMessage(){
    
  }
}
