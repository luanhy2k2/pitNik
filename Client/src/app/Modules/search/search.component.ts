import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UserService } from 'src/app/services/User.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  constructor(
    private readonly route:ActivatedRoute,
    private readonly UserService:UserService,
    private readonly FriendService:FriendShipService
  ) {}
  keyword:string = "";
  Users:BaseQueriesResponse<Account> = {
    pageIndex: 1,
    pageSize:10,
    items:[],
    keyword: "",
    total: 0
  }
  CreateFriend:CreateFriendShip = {
    receiverId: "",
    senderUserName: "",
    status: FriendshipStatus.Pending,
    requestedAt:new Date()
  }
  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      this.keyword = params['keyword'];
    })
  }
  LoadAccount(){
    this.Users.keyword = this.keyword;
    this.UserService.getPagedData(this.Users.pageIndex,this.Users.pageSize, this.Users.keyword).subscribe(
      res =>{
        this.Users.items = res.items,
        this.Users.pageIndex = res.pageIndex,
        this.Users.pageSize = res.pageSize,
        this.Users.total = res.total,
        this.Users.keyword = res.keyword
      }
    )
  }
  CreateFriendShip(receiverId:string){
    this.CreateFriend.receiverId = receiverId;
    this.FriendService.create(this.CreateFriend).subscribe(res =>{
      if(res.success == true){
        alert(res.message)
      }
    })
  }
}
