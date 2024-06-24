import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { UserService } from './User.service';
import { CreateComment } from '../Models/Comment/create-comment.entity';
import { Comment } from '../Models/Comment/comment.entity';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private connection!: signalR.HubConnection;
  constructor(private userService: UserService) { }
  startConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7261/chatHub", {
        accessTokenFactory: () =>{ return this.userService.getUser().token}
      }).withAutomaticReconnect().configureLogging(signalR.LogLevel.Information)
      .build();
    return this.connection.start().catch(err => console.error('Error while starting connection: ' + err));;
  }
  stopConnection = () => {
    if (this.connection) {
      this.connection.stop();
      console.log('Đã ngắt kết nối');
    }
  }
  addNewMessageListener(callback: (messageView: any) => void) {
    this.connection.on("newMessage", callback);
  }
  async addProfileInfoListener(callback: (displayName: any) => void) {
    this.connection.on("getProfileInfo", callback);
  }
  async addUserConnectedListener(callback: (id: any) => void) {
    this.connection.on("addUserConnected", callback);
  }
  async addRemoveUserListener(callback: (user: any) => void) {
    this.connection.on("removeUser", callback);
  }
  async addAddUserListener(callback: (user: any) => void) {
    this.connection.on("addUser", callback);
  }
  async addChatRoomListener(callback: (room: any) => void) {
    this.connection.on("addChatRoom", callback);
  }
  async addPostListener(callback: (post: any) => void) {
    this.connection.on("newPost", callback);
  }
  async addReactListener(callback: (react: any) => void) {
    this.connection.on("addReact", callback);
  }
  async addCommentLister(callback: (comment: Comment) => void) {
    this.connection.on("addComment", callback);
  }
  async addFriendPendingLister(callback: (FriendPending: CreateFriendShip) => void) {
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
  async Leave(roomName: string) {
    try {
      return await this.connection.invoke("Leave", roomName);
      console.log("leave success!", roomName)
    } catch (err) {
      console.error('Error while leaving room:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  async getUserList() {
    try {
      return await this.connection.invoke("GetUsers");
    } catch (err) {
      console.error('Error while get usser:', err);
      throw err; // Re-throw the error to be handled by the caller if necessary
    }
  }
  
}
