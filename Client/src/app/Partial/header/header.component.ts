import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Account, Gender } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Conversation } from 'src/app/Models/Conversation/Conversation.entity';
import { CreateConversation } from 'src/app/Models/Conversation/CreateConversation.entity';
import { FriendShip, FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UpdateStatusFriend } from 'src/app/Models/FriendShip/UpdateStatusFriend.entity';
import { CreateMessage } from 'src/app/Models/Message/CreateMessage.entity';
import { Message } from 'src/app/Models/Message/Message.entity';
import { UpdateMessageReadStatus } from 'src/app/Models/Message/UpdateMessageReadStatus.entuty';
import { Notification } from 'src/app/Models/Notification/Notification.entity';
import { UpdateStatusReadNotification } from 'src/app/Models/Notification/UpdateStatusReadNotification.entity';
import { UserService } from 'src/app/services/User.service';
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
export class HeaderComponent {
  constructor(private readonly FriendService:FriendShipService,
    private readonly signalRService:SignalRService,
    private readonly userService:UserService,
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
  Messages:BaseQueriesResponse<Message> = {
    pageIndex:1,
    pageSize:20,
    keyword:"",
    items:[],
    total:0
  };
  imageMessageSrcs: (string | ArrayBuffer | null)[] = [];
  CreateMessageRequest:CreateMessage = {
    conversationId:0,
    content:"",
    files:[]
  };
  toggleEmoji:boolean = false;
  updateMessageReadStatus:UpdateMessageReadStatus = {
    userId:"",
    conversationId:0,
    status:false
  }
  createConversationReq:CreateConversation = {
    otherMembersId:[]
  };
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
    this.messageService.loadConversation$.subscribe(userId => {
      if(userId != ""){
        this.conversationService.getByFriendId(userId).subscribe(res =>{
          if(res != null){
            this.LoadMessageConverstion(res.id);
          }
          else{
            this.createConversationReq.otherMembersId.push(userId);
            this.conversationService.createConversation(this.createConversationReq).subscribe(conversation =>{
              if(conversation.success == true){
                this.signalRService.joinRoom(conversation.object.id.toString())
                this.LoadMessageConverstion(conversation.object.id);
              }
            })
          }
        })
      }
      
    });

    if(this.userService.getUser().token){
      this.LoadNotification();
      this.LoadConversation();
      this.LoadFrienPending();
    }
    this.signalRService.friendInvitationAdded$.subscribe(res =>{
      this.LoadFrienPending();
    })
    this.signalRService.notificationAdded$.subscribe(res =>{
      this.Notifications.items.unshift(res);
    })
    this.signalRService.profileAdded$.subscribe(res =>{
      this.user = res;
      console.log("currentUser:", res);
    })
    this.signalRService.listFriendIdConnected$.subscribe(res => {
      console.log("ListFriendConnected", res);
      for (let userId of res) {
          for (let item of this.Conversations.items) {
              if (item.member.some(x => x.id === userId)) {
                  item.isOnline = true;
                  this.signalRService.joinRoom(item.id.toString())
              }
          }
      }
    });
    this.signalRService.userConnectedAdd$.subscribe(res =>{
      console.log("add friend connected:", res);
      console.log(this.Conversations.items)
      for(let item of this.Conversations.items){
        if(item.member.find(x =>x.id == res)){
          item.isOnline = true;
          this.signalRService.joinRoom(item.id.toString())
        }
      }
    })
    this.signalRService.messageAdded$.subscribe(message =>{
      for (let element of this.Conversations.items) {
        if (element.id == message.conversationId) {
          element.message = message.content;
          element.timeMessage = message.created;
          element.isSeen = message.sender.id == this.user.id
          break;
        }
      }
      message.isSentByCurrentUser = message.sender.id == this.user.id
      message.created = "";
      this.Messages.items.push(message); 
    })
  }
  
  addEmoji(emoji: string) {
    this.CreateMessageRequest.content += emoji;
  }
  
  ToggleEmoji(){
    this.toggleEmoji = !this.toggleEmoji;
  }
  onFilesSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files) {
      Array.from(fileInput.files).forEach(file => {
        this.CreateMessageRequest.files.push(file); // Update CreatePost object
        if (file.type.startsWith('image/')) {
          const reader = new FileReader();
          reader.onload = (e: ProgressEvent<FileReader>) => {
            if (e.target) {
              this.imageMessageSrcs.push(e.target.result);
            }
          };
          reader.readAsDataURL(file);
        }
      });
    }
    console.log(this.imageMessageSrcs);
  }
  CreateMessage(){
    this.messageService.create(this.CreateMessageRequest).subscribe(res =>{
      if(res.success == true){
        this.CreateMessageRequest.content = "";
        this.imageMessageSrcs = [];
        for (let element of this.Conversations.items) {
          if (element.id == res.object.conversationId && element.isOnline != true) {
            element.message = res.object.content;
            element.timeMessage = res.object.created;
            element.isSeen = res.object.sender.id == this.user.id;
            res.object.isSentByCurrentUser = res.object.sender.id == this.user.id
            res.object.created = "";
            this.Messages.items.push(res.object); 
            break;
          }
        }
      }
    })
  }
  LoadMessageConverstion(conversionId:number){
    this.isHidden = false;
    this.CreateMessageRequest.conversationId = conversionId;
    for(let item of this.Conversations.items){
      if(item.id == conversionId && item.isSeen == false){
        this.updateMessageReadStatus.conversationId = conversionId;
        this.updateMessageReadStatus.status = true;
        this.messageService.updateMessageReadStatus(this.updateMessageReadStatus).subscribe(res =>{
          if(res.success == true){
            item.isSeen = true;
          }
        })
        break;
      }
    }
    this.messageService.getPagedData(this.Messages.pageIndex,this.Messages.pageSize, conversionId,this.Messages.keyword).subscribe(
      res =>{
        res.items.forEach(item =>{
          item.isSentByCurrentUser = item.sender.id == this.user.id;
        })
        this.Messages.items = res.items;
      })
  }
  HiddenModalMesage(){
    this.isHidden = true; 
  }
  UpdateFriendStatus(id:number, status:FriendshipStatus){
    this.UpdateFriendStatusModel.id = id;
    this.UpdateFriendStatusModel.status = status;
    this.FriendService.Update(this.UpdateFriendStatusModel).subscribe(res =>{
      if(res.success == true){
        alert(res.message);
        for (let i = 0; i < this.FriendPending.items.length; i++) {
          if (this.FriendPending.items[i].id === id) {
              this.FriendPending.items.splice(i, 1); 
              break;
          }
        }
        if(status == FriendshipStatus.Accepted){
          this.LoadConversation();
        }
      }
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
  UnreadConversationCount(): number {
    return this.Conversations.items.filter(item => !item.isSeen).length;
  }
  CountFriendShipPending():number{
    return this.FriendPending.items.length;
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
          }
        }
      }
    })
  }
  ngOnDestroy(){
    this.signalRService.stopConnection();
  }
}
