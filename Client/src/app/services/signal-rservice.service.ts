import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { UserService } from './User.service';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { Comment } from '../Models/Comment/comment.entity';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';
import { UpdateStatusFriend } from '../Models/FriendShip/UpdateStatusFriend.entity';
import { Notification } from '../Models/Notification/Notification.entity';
import { Message } from '../Models/Message/Message.entity';
import { FriendShip } from '../Models/FriendShip/FriendShip.entity';
import { CreateInteraction } from '../Models/Interaction/CreateInteraction.entity';
import { Post } from '../Models/Post/Post.entity';
import { CreatePost } from '../Models/Post/CreatePost.entity';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private connection: signalR.HubConnection;

  private profileAddedSource = new Subject<any>();
  private postAddedSource = new Subject<CreatePost>();
  private reactAddedSource = new Subject<CreateInteraction>();
  private commentAddedSource = new Subject<Comment>();
  private notificationAddedSource = new Subject<Notification>();
  private messageAddedSource = new Subject<Message>();
  private friendStatusUpdatedSource = new Subject<UpdateStatusFriend>();
  private friendInvitationAddedSource = new Subject<CreateFriendShip>();
  private userConnectedAddedSource = new Subject<any>();

  profileAdded$ = this.profileAddedSource.asObservable();
  postAdded$ = this.postAddedSource.asObservable();
  reactAdded$ = this.reactAddedSource.asObservable();
  commentAdded$ = this.commentAddedSource.asObservable();
  notificationAdded$ = this.notificationAddedSource.asObservable();
  messageAdded$ = this.messageAddedSource.asObservable();
  friendStatusUpdateAdded$ = this.friendStatusUpdatedSource.asObservable();
  friendInvitationAdded$ = this.friendInvitationAddedSource.asObservable();
  userConnectedAdd$ = this.userConnectedAddedSource.asObservable();

  constructor(private userService: UserService) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7261/chatHub', {
        accessTokenFactory: () => {
          const user = this.userService.getUser();
          return user ? user.token : '';
        },
        transport: signalR.HttpTransportType.LongPolling,
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }

  startConnection() {
    try {
      this.connection.start();
      console.log('Đã kết nối SignalR');
      this.registerSignalREvents();
    } catch (err) {
      console.error('Lỗi khi bắt đầu kết nối: ', err);
    }
  }

  async stopConnection() {
    if (this.connection) {
      try {
        await this.connection.stop();
        console.log('Đã ngắt kết nối SignalR');
      } catch (err) {
        console.error('Lỗi khi dừng kết nối: ', err);
      }
    }
  }
  async joinRoom(roomName: string) {
    try {
      return await this.connection.invoke("Join", roomName);
    } catch (err) {
      console.error('Error while joining room:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  private registerSignalREvents() {
    this.connection.on('getProfileInfo', (react) => {
      this.profileAddedSource.next(react);
    });
    this.connection.on('addUserConnected', (react) => {
      this.userConnectedAddedSource.next(react);
    });
    this.connection.on('createNotification', (react) => {
      this.notificationAddedSource.next(react);
    });
    this.connection.on('newMessage', (message) => {
      console.log("message",message)
      this.messageAddedSource.next(message);
    });
    this.connection.on('newPost', (post) => {
      this.postAddedSource.next(post);
    });
    this.connection.on('addReact', (react) => {
      this.reactAddedSource.next(react);
    });
    this.connection.on('addComment', (comment) => {
      this.commentAddedSource.next(comment);
    });
    this.connection.on('updateFriend', (react) => {
      this.friendStatusUpdatedSource.next(react);
    });
    this.connection.on('addFriendship', (react) => {
      this.friendInvitationAddedSource.next(react);
    });
    
  }
}
