import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Account } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Conversation } from 'src/app/Models/Conversation/Conversation.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendShip, FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UpdateStatusFriend } from 'src/app/Models/FriendShip/UpdateStatusFriend.entity';
import { Message } from 'src/app/Models/Message/Message.entity';
import { Notification } from 'src/app/Models/Notification/Notification.entity';
import { UpdateStatusReadNotification } from 'src/app/Models/Notification/UpdateStatusReadNotification.entity';
import { ChatHubService } from 'src/app/services/chatHub.service';
import { ConversationService } from 'src/app/services/conversation.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { MessageService } from 'src/app/services/message.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(private readonly FriendService:FriendShipService,
     private readonly chatHubService:ChatHubService,
     private readonly notificationService:NotificationService,
     private readonly conversationService:ConversationService,
     private readonly messageService:MessageService,
     private readonly Router:Router){}
  isHidden: boolean = true;
  FriendPending:BaseQueriesResponse<FriendShip> = {
    pageIndex: 1,
    pageSize: 10,
    items:[],
    total:0,
    keyword:""
  }
  UpdateFriendStatusModel:UpdateStatusFriend = {
    id: 0,
    status:FriendshipStatus.Pending,
    requestedAt:new Date
  }
  user:Account = {
    id:"",
    name:"",
    address:"",
    image:"",
    userName:"",
    phoneNumber:"",
    email:""
  }
  Notifications:BaseQueriesResponse<Notification> = {
    pageIndex:1,
    pageSize:10,
    items:[],
    total:0,
    keyword:""
  }
  UpdateNotification:UpdateStatusReadNotification = {
    id: 0,
    status: false
  }
  Conversations:BaseQueriesResponse<Conversation> = {
    pageIndex: 1,
    pageSize:10,
    keyword:"",
    total: 0,
    items:[]
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
    this.chatHubService.startConnection();
    this.chatHubService.addFriendPendingLister((createFriendShipDto: CreateFriendShip) => {
      this.LoadFrienPending();
    }); 
    this.chatHubService.addNotificationListener((Notification: Notification) => {
      this.LoadNotification();
    }); 
    this.chatHubService.addProfileInfoListener((user:any) =>{
      this.user = user;
    })
    this.chatHubService.updateFriendStatusLister((dto: UpdateStatusFriend) => {
      this.LoadFrienPending();
    }); 
    this.LoadNotification();
    this.LoadConversation();
  }
  Messages:BaseQueriesResponse<Message> = {
    pageIndex:1,
    pageSize:10,
    keyword:"",
    items:[],
    total:0
  }
  LoadMessageConverstion(conversionId:number){
    this.isHidden = !this.isHidden;
    this.messageService.getPagedData(this.Messages.pageIndex,this.Messages.pageSize, conversionId,this.Messages.keyword).subscribe(
      res =>{
        this.Messages.items = res.items
      }
    )
  }
  UpdateFriendStatus(id:number, status:FriendshipStatus){
    this.UpdateFriendStatusModel.id = id;
    this.UpdateFriendStatusModel.status = status;
    this.FriendService.Update(this.UpdateFriendStatusModel).subscribe(res =>{
      alert(res.message)
    })
  }
  logOut() {
    localStorage.removeItem('user');
    alert("Đăng xuất thành công");
    this.chatHubService.stopConnection();
    this.Router.navigate(['/login']);
  }
  LoadNotification(){
    this.notificationService.getPagedData(this.Notifications.pageIndex, this.Notifications.pageSize,this.Notifications.keyword).subscribe(
      res =>{
        this.Notifications.pageIndex = res.pageIndex,
        this.Notifications.pageSize = res.pageSize,
        this.Notifications.items = res.items,
        this.Notifications.total = res.total
      }
    )
  }
  UnreadNotificationCount(): number {
    return this.Notifications.items.filter(item => !item.isSeen).length;
  }
  LoadConversation(){
    this.conversationService.getPagedData(this.Conversations.pageIndex, this.Conversations.pageSize, this.Conversations.keyword).subscribe(res =>{
      this.Conversations.items = res.items;
    })
  }
  UpdateReadStatusNotification(id:number, status:boolean){
    this.UpdateNotification.id = id;
    this.UpdateNotification.status = status;
    this.notificationService.UpdateReadStatus(this.UpdateNotification).subscribe(res =>{
      if(res.success == true){
        for (let element of this.Notifications.items) {
          if (element.id == this.UpdateNotification.id) {
            element.isSeen = this.UpdateNotification.status;
            break;
          }
        }
      }
    })
  }
  sendNewMessage(){
    
  }
}
