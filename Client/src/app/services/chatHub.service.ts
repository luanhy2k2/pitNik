import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { UserService } from './User.service';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { Comment } from '../Models/Comment/comment.entity';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';
import { UpdateStatusFriend } from '../Models/FriendShip/UpdateStatusFriend.entity';
import { Notification } from '../Models/Notification/Notification.entity';
import { Message } from '../Models/Message/Message.entity';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7261/chatHub", {
    accessTokenFactory: () => {
      let token = this.userService.getUser().token;
        return token ?? '';
    },
    transport: signalR.HttpTransportType.LongPolling
  })
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();
  constructor(private userService: UserService) {
  }
  startConnection() {
    try {
      this.connection.start();
      console.log('SignalR connected');
    } catch (err) {
      console.error('Error while starting connection: ', err);
    }
  }
  stopConnection() {
    if (this.connection) {
      this.connection.stop();
      console.log('Disconnected from SignalR');
    }
  }
  addNewMessageListener(callback: (messageDto: Message) => void) {
    this.connection.on("newMessage", callback);
  }
  addProfileInfoListener(callback: (user: any) => void) {
    this.connection.on("getProfileInfo", callback);
  }
  addUserConnectedListener(callback: (id: any) => void) {
    this.connection.on("addUserConnected", callback);
  }
  addPostListener(callback: (post: any) => void) {
    this.connection.on("newPost", callback);
  }
  addNotificationListener(callback: (Notification: Notification) => void) {
    this.connection.on("createNotification", callback);
  }
  addReactListener(callback: (react: any) => void) {
    this.connection.on("addReact", callback);
  }
  addCommentLister(callback: (comment: any) => void) {
    this.connection.on("addComment", callback);
  }
  updateFriendStatusLister(callback: (UpdateFriendShipDto: UpdateStatusFriend) => void) {
    this.connection.on("updateFriend", callback);
  }
  addFriendPendingLister(callback: (createFriendShipDto: any) => void) {
    this.connection.on("addFriendship", callback);
  }

  async joinRoom(roomName: string) {
    try {
      return await this.connection.invoke("Join", roomName);
    } catch (err) {
      console.error('Error while joining room:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }

  async leaveRoom(roomName: string) {
    try {
      return await this.connection.invoke("Leave", roomName);
    } catch (err) {
      console.error('Error while leaving room:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }

  async getUserList() {
    try {
      return await this.connection.invoke("GetUsers");
    } catch (err) {
      console.error('Error while getting users:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
}
