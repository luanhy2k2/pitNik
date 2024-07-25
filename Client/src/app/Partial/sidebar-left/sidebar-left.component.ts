import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Account, Gender } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Conversation } from 'src/app/Models/Conversation/Conversation.entity';
import { CreateConversation } from 'src/app/Models/Conversation/CreateConversation.entity';
import { MyFriend } from 'src/app/Models/FriendShip/Myfriend.entity';
import { CreateMessage } from 'src/app/Models/Message/CreateMessage.entity';
import { Message } from 'src/app/Models/Message/Message.entity';
import { UserService } from 'src/app/services/User.service';
import { ConversationService } from 'src/app/services/conversation.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { MessageService } from 'src/app/services/message.service';
import { SignalRService } from 'src/app/services/signal-rservice.service';

@Component({
  selector: 'app-sidebar-left',
  templateUrl: './sidebar-left.component.html',
  styleUrls: ['./sidebar-left.component.css']
})
export class SidebarLeftComponent {
  constructor(private readonly signalRService: SignalRService,
    private readonly friendShervice: FriendShipService,
    private readonly messageService:MessageService,
    private readonly Router: Router) { }
  friends: BaseQueriesResponse<MyFriend> = {
    items: [],
    total: 0,
    pageIndex: 1,
    pageSize: 15,
    keyword: ""
  }
  logOut() {
    localStorage.removeItem('user');
    alert("Đăng xuất thành công");
    this.signalRService.stopConnection();
    this.Router.navigate(['/login']);
  }


  openChatModal(userId:string) {
    this.messageService.toggleModal(userId);
  }
  
  ngOnInit() {
    this.loadMyFriend();
    this.signalRService.listFriendIdConnected$.subscribe(res => {
      console.log("ListFriendConnected", res);
      for (let userId of res) {
        for (let item of this.friends.items) {
          if (item.userId === userId) {
            item.isOnline = true;
          }
        }
      }
    });
    this.signalRService.userOnDisconnected$.subscribe(res => {
      console.log(res);
      for (let item of this.friends.items) {
        if (item.userId === res) {
          item.isOnline = !item.isOnline;
          break;
        }
      }
    })
    this.signalRService.userConnectedAdd$.subscribe(res => {
      console.log("add friend connected:", res);
      console.log(this.friends.items)
      for (let item of this.friends.items) {
        if (item.userId == res) {
          item.isOnline = true;
        }
      }
    })
  }
  loadMyFriend() {
    this.friendShervice.GetFriendOfUser( this.friends.pageIndex, this.friends.pageSize, this.friends.keyword).subscribe(res => {
      this.friends = res;
    })
  }
}
