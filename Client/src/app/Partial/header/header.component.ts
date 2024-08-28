import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { userImageUrl } from 'src/app/Environments/env';
import { Account, Gender } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Conversation, StatusConversation } from 'src/app/Models/Conversation/Conversation.entity';
import { CreateConversation } from 'src/app/Models/Conversation/CreateConversation.entity';
import { FriendShip, FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { UpdateStatusFriend } from 'src/app/Models/FriendShip/UpdateStatusFriend.entity';
import { CreateMessage } from 'src/app/Models/Message/CreateMessage.entity';
import { Message } from 'src/app/Models/Message/Message.entity';
import { UpdateMessageReadStatus } from 'src/app/Models/Message/UpdateMessageReadStatus.entuty';
import { CreateNotification } from 'src/app/Models/Notification/CreateNotification';
import { Notification } from 'src/app/Models/Notification/Notification.entity';
import { UpdateStatusReadNotification } from 'src/app/Models/Notification/UpdateStatusReadNotification.entity';
import { UserService } from 'src/app/services/User.service';
import { ConversationService } from 'src/app/services/conversation.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { MessageService } from 'src/app/services/message.service';
import { NotificationService } from 'src/app/services/notification.service';
import { PresenceService } from 'src/app/services/presence.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  public userImageUrl = userImageUrl;
  constructor(private readonly FriendService: FriendShipService,
    private readonly presenceService: PresenceService,
    private readonly notificationService: NotificationService,
    private readonly conversationService: ConversationService,
    private readonly messageService: MessageService,
    private readonly Router: Router) { }
  isHidden: boolean = true;
  FriendPending: BaseQueriesResponse<FriendShip> = {
    pageIndex: 1,
    pageSize: 10,
    items: [],
    total: 0,
    keyword: ""
  }
  UpdateFriendStatusModel: UpdateStatusFriend = {
    id: 0,
    status: FriendshipStatus.Pending,
    requestedAt: new Date
  }
  user: Account = {
    id: "",
    name: "",
    address: "",
    image: "",
    userName: "",
    phoneNumber: "",
    email: "",
    birthday: new Date,
    gender: Gender.Male
  }
  Notifications: BaseQueriesResponse<Notification> = {
    pageIndex: 1,
    pageSize: 20,
    items: [],
    total: 0,
    keyword: ""
  }
  Conversations: BaseQueriesResponse<Conversation> = {
    pageIndex: 1,
    pageSize: 20,
    keyword: "",
    total: 0,
    items: []
  }
  keyword: string = "";
  Messages: BaseQueriesResponse<Message> = {
    pageIndex: 1,
    pageSize: 20,
    keyword: "",
    items: [],
    total: 0
  };
  uploadImageMessage:File[] = [];
  imageMessageSrcs: (string | ArrayBuffer | null)[] = [];
  CreateMessageRequest: CreateMessage = {
    conversationId: 0,
    content: "",
    // files: []
  };
  currentConversationStatus:StatusConversation = {
    conversationNane: "",
    isOnline:false
  }
  toggleEmoji: boolean = false;
  updateMessageReadStatus: UpdateMessageReadStatus = {
    userId: "",
    conversationId: 0,
    status: false
  }
  createConversationReq: CreateConversation = {
    otherMembersId: []
  };
  Search() {
    this.Router.navigate(['/search'], { queryParams: { keyword: this.keyword } });
  }
  LoadFrienPending() {
    this.FriendService.getPagedData(this.FriendPending.pageIndex, this.FriendPending.pageSize, this.FriendPending.keyword).subscribe(
      res => {
        this.FriendPending.items = res.items,
          this.FriendPending.total = res.total
      },
      err => {
        alert("Đã có lỗi xảy ra!")
      }
    )
  }
  ngOnInit() {
    // this.presenceService.startConnection();
    this.messageService.loadConversation$.subscribe(userId => {
      if (userId != "") {
        this.conversationService.getByFriendId(userId).subscribe(res => {
          if (res != null) {
            this.LoadMessageConverstion(res.id);
          }
          else {
            this.createConversationReq.otherMembersId.push(userId);
            this.conversationService.createConversation(this.createConversationReq).subscribe(conversation => {
              if (conversation.success == true) {
                this.messageService.joinRoom(conversation.object.id.toString())
                this.LoadMessageConverstion(conversation.object.id);
              }
            })
          }
        })
      }

    });
    this.LoadNotification();
    this.LoadConversation();
    this.LoadFrienPending();
    this.presenceService.friendInvitationAdded$.subscribe(res => {
      this.LoadFrienPending();
    })
    this.presenceService.notificationAdded$.subscribe(res => {
      this.Notifications.items.unshift(res);
    })
    this.presenceService.profileAdded$.subscribe(res => {
      this.user = res;
    })
    this.presenceService.listFriendIdConnected$.subscribe(res => {
      for (let userId of res) {
        for (let item of this.Conversations.items) {
          if (item.member.some(x => x.id === userId)) {
            item.isOnline = true;
            this.messageService.joinRoom(item.id.toString())
          }
        }
      }
    });
    this.presenceService.userConnectedAdd$.subscribe(res => {
      for (let item of this.Conversations.items) {
        if (item.member.find(x => x.id == res)) {
          item.isOnline = true;
          this.messageService.joinRoom(item.id.toString());
        }
      }
    })
    this.messageService.startConnection();
    this.messageService.messageAdded$.subscribe(message => {
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
    this.presenceService.userOnDisconnected$.subscribe(res => {
      for (let item of this.Conversations.items) {
        if (item.member.find(x =>x.isCurrentUser == false)?.id === res) {
          item.isOnline = !item.isOnline;
          break;
        }
      }
    })
  }

  addEmoji(emoji: string) {
    this.CreateMessageRequest.content += emoji;
  }

  ToggleEmoji() {
    this.toggleEmoji = !this.toggleEmoji;
  }
  onFilesSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files) {
      Array.from(fileInput.files).forEach(file => {
        this.uploadImageMessage.push(file); // Update CreatePost object
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
  }
  SendMessage() {
    if(this.uploadImageMessage && this.uploadImageMessage.length > 0){
      this.messageService.UploadFile(this.uploadImageMessage).subscribe(res =>{
        this.CreateMessageRequest.content = res.object;
        this.CreateMessage();
      })
    }
    else{
      this.CreateMessage();
    }
    
  }
  CreateMessage(){
    this.messageService.sendMessage(this.CreateMessageRequest)
    .then(res => {
      this.CreateMessageRequest.content = "";
      this.imageMessageSrcs = [];
      for (let element of this.Conversations.items) {
        if (element.id == this.CreateMessageRequest.conversationId && element.isOnline != true) {
          element.message = this.CreateMessageRequest.content;
          element.isSeen = true;
          res.isSentByCurrentUser = true;
          res.created = "";
          this.Messages.items.push(res);
          break;
        }
      }
    })
    .catch(err => console.error('Error sending message: ' + err));
  }
  LoadMessageConverstion(conversionId: number) {
    this.isHidden = false;
    this.CreateMessageRequest.conversationId = conversionId;
    for (let item of this.Conversations.items) {
      if (item.id == conversionId) {
        this.currentConversationStatus.conversationNane = item.member.find(x => x.isCurrentUser === false)?.name || '';
        this.currentConversationStatus.isOnline = item.isOnline
      }
      if (item.id == conversionId && item.isSeen == false) {
        this.updateMessageReadStatus.conversationId = conversionId;
        this.updateMessageReadStatus.status = true;
        this.messageService.UpdateMessageReadStatus(this.updateMessageReadStatus).then(res => {
          item.isSeen = true;
        })
        break;
      }
    }
    this.messageService.getPagedData(this.Messages.pageIndex, this.Messages.pageSize, conversionId, this.Messages.keyword).subscribe(
      res => {
        res.items.forEach(item => {
          item.isSentByCurrentUser = item.sender.id == this.user.id;
        })
        this.Messages.items = res.items;
      })
  }
  HiddenModalMesage() {
    this.isHidden = true;
  }
  UpdateFriendStatus(id: number, status: FriendshipStatus) {
    this.UpdateFriendStatusModel.id = id;
    this.UpdateFriendStatusModel.status = status;
    this.FriendService.Update(this.UpdateFriendStatusModel).subscribe(res => {
      if (res.success == true) {
        alert(res.message);
        for (let i = 0; i < this.FriendPending.items.length; i++) {
          if (this.FriendPending.items[i].id === id) {
            var notification:CreateNotification = {
              content:"Đã chấp nhận lời mời kết bạn của bạn",
              receiverId: this.FriendPending.items[i].senderId
            }
            if(status == FriendshipStatus.Rejected)
              notification.content = "Đã từ chối lời mời kết bạn của bạn"
            this.presenceService.sendNotification(notification);
            this.FriendPending.items.splice(i, 1);
            break;
          }
        }
        if (status == FriendshipStatus.Accepted) {
          this.LoadConversation();
        }
      }
    })
  }
  logOut() {
    localStorage.removeItem('user');
    alert("Đăng xuất thành công");
    this.presenceService.stopConnection();
    this.Router.navigate(['/login']);
  }
  LoadNotification() {
    this.notificationService.getPagedData(this.Notifications.pageIndex, this.Notifications.pageSize, this.Notifications.keyword).subscribe(
      res => {
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
  CountFriendShipPending(): number {
    return this.FriendPending.items.length;
  }
  LoadConversation() {
    this.conversationService.getPagedData(this.Conversations.pageIndex, this.Conversations.pageSize, this.Conversations.keyword).subscribe(res => {
      this.Conversations.items = res.items;
    })

  }
  UpdateReadStatusNotification() {
    this.notificationService.UpdateReadStatus().subscribe(res => {
      if (res.success == true) {
        const unreadNotifications = this.Notifications.items.filter(notification => !notification.isSeen);
        unreadNotifications.forEach(notification => {
          notification.isSeen = true;
          this.UnreadNotificationCount();
        });
      }
    });
    
  }
  ngOnDestroy() {
    this.presenceService.stopConnection();
  }
}
