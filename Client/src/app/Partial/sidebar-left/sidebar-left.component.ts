import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { userImageUrl } from 'src/app/Environments/env';
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
import { PresenceService } from 'src/app/services/presence.service';

@Component({
  selector: 'app-sidebar-left',
  templateUrl: './sidebar-left.component.html',
  styleUrls: ['./sidebar-left.component.css']
})
export class SidebarLeftComponent {
  public userImageUrl = userImageUrl;
  constructor(private readonly PresenceService: PresenceService,
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
    this.PresenceService.stopConnection();
    this.Router.navigate(['/login']);
  }


  openChatModal(userId:string) {
    this.messageService.toggleModal(userId);
  }
  
  ngOnInit() {
    this.loadMyFriend();
    this.PresenceService.listFriendIdConnected$.subscribe(res => {
      const connectedUserIds = new Set(res);
      for (let i = 0; i<=this.friends.items.length; i++) {
        this.friends.items[i].isOnline = connectedUserIds.has(this.friends.items[i].userId);
      }
    });
    this.PresenceService.userOnDisconnected$.subscribe(res => {
      for (let item of this.friends.items) {
        if (item.userId === res) {
          item.isOnline = !item.isOnline;
          break;
        }
      }
    })
    this.PresenceService.userConnectedAdd$.subscribe(res => {
      for (let item of this.friends.items) {
        if (item.userId == res) {
          item.isOnline = true;
          break;
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
