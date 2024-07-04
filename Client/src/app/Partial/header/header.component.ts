import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Account, Gender } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Conversation } from 'src/app/Models/Conversation/Conversation.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendShip, FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UpdateStatusFriend } from 'src/app/Models/FriendShip/UpdateStatusFriend.entity';
import { CreateMessage } from 'src/app/Models/Message/CreateMessage.entity';
import { Message } from 'src/app/Models/Message/Message.entity';
import { Notification } from 'src/app/Models/Notification/Notification.entity';
import { UpdateStatusReadNotification } from 'src/app/Models/Notification/UpdateStatusReadNotification.entity';
import { UserService } from 'src/app/services/User.service';
import { ChatHubService } from 'src/app/services/chatHub.service';
import { ConversationService } from 'src/app/services/conversation.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { MessageService } from 'src/app/services/message.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SignalRService } from 'src/app/services/signal-rservice.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnDestroy {
  constructor(private readonly FriendService:FriendShipService,
    private readonly signalRService:SignalRService,
     private readonly notificationService:NotificationService,
     private readonly conversationService:ConversationService,
     private readonly messageService:MessageService,
     private readonly userService:UserService,
     private readonly Router:Router){}
  ngOnDestroy(): void {
    alert("dé")
  }
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
    email:"",
    birthday:new Date,
    gender:Gender.Male
  }
  Notifications:BaseQueriesResponse<Notification> = {
    pageIndex:1,
    pageSize:20,
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
    pageSize:20,
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
    this.signalRService.friendInvitationAdded$.subscribe(res =>{
      this.LoadFrienPending();
    })
    this.signalRService.notificationAdded$.subscribe(res =>{
      this.LoadNotification();
    })
    this.signalRService.profileAdded$.subscribe(res =>{
      this.user = res;
      this.LoadNotification();
      this.LoadConversation();
    })
    this.signalRService.friendStatusUpdateAdded$.subscribe(res =>{
      this.LoadFrienPending();
    })
    this.signalRService.messageAdded$.subscribe(message =>{
      console.log("message:", message);
      message.isSentByCurrentUser = message.sender.id == this.user.id
      this.Messages.items.push(message); 
      for (let element of this.Conversations.items) {
        if (element.id == message.conversationId) {
          element.message = message.content;
          element.timeMessage = message.created;
          element.isSeen = message.isSentByCurrentUser
          break;
        }
      }
    })
  }
  Messages:BaseQueriesResponse<Message> = {
    pageIndex:1,
    pageSize:20,
    keyword:"",
    items:[],
    total:0
  };
  CreateMessageRequest:CreateMessage = {
    receiverId: "",
    senderUserName:"",
    conversationId:0,
    content:""
  }
  CreateMessage(){
    this.messageService.create(this.CreateMessageRequest).subscribe(res =>{
      if(res.success == true){
        res.object.isSentByCurrentUser = res.object.sender.id == this.user.id
        this.Messages.items.push(res.object); 
        for (let element of this.Conversations.items) {
        if (element.id == res.object.conversationId) {
          element.message = res.object.content;
          element.timeMessage = res.object.created;
          element.isSeen = res.object.isSentByCurrentUser
          break;
        }
      }
        this.CreateMessageRequest.content = "";
      }
    })
    
  }
  LoadMessageConverstion(conversionId:number, idUser:string){
    this.isHidden = !this.isHidden;
    this.CreateMessageRequest.receiverId = idUser;
    this.CreateMessageRequest.conversationId = conversionId;
    this.messageService.getPagedData(this.Messages.pageIndex,this.Messages.pageSize, conversionId,this.Messages.keyword).subscribe(
      res =>{
        res.items.forEach(item =>{
          item.isSentByCurrentUser = item.sender.id == this.user.id;
          this.Messages.items.push(item)
        })
      })
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
    this.signalRService.stopConnection();
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
