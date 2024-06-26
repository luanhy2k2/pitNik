import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private readonly FriendService:FriendShipService,
     private readonly chatHubService:ChatHubService,
    private readonly Router:Router){}
  isHidden: boolean = true;
  FriendPending:BaseQueriesResponse<FriendShip> = {
    pageIndex: 1,
    pageSize: 10,
    items:[],
    total:0,
    keyword:""
  }
  keyword:string = "";
  Search() {
    this.Router.navigate(['/search'],{ queryParams: { keyword: this.keyword } });
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
    this.chatHubService.addFriendPendingLister((post: any) => {
      this.LoadFrienPending();
    }); 
  }
  chatMessages:any;
  message:any
  toggleDiv() {
    this.isHidden = !this.isHidden;
  }
  logOut() {
    localStorage.removeItem('user');
    alert("Đăng xuất thành công");
    this.chatHubService.stopConnection();
   
  }
  sendNewMessage(){
    
  }
}
