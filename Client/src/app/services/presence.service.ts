import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { UserService } from './User.service';
import * as signalR from '@microsoft/signalr';
import { apiUrl } from '../Environments/env';
import { CreateFriendShip } from '../Models/FriendShip/CreateFriendShip.entity';
import { Notification } from '../Models/Notification/Notification.entity';
import { Account } from '../Models/Account/Account.entity';
import { CreateNotification } from '../Models/Notification/CreateNotification';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  private connection: signalR.HubConnection;
  private profileAddedSource = new Subject<Account>();
  private userOnDisconnectedSource = new Subject<string>();
  private notificationAddedSource = new Subject<Notification>();
  private userConnectedAddedSource = new Subject<any>();
  private listFriendIdConnectedSource = new Subject<any>();
  private friendInvitationAddedSource = new Subject<CreateFriendShip>();
  profileAdded$ = this.profileAddedSource.asObservable();
  userOnDisconnected$ = this.userOnDisconnectedSource.asObservable();
  notificationAdded$ = this.notificationAddedSource.asObservable();
  userConnectedAdd$ = this.userConnectedAddedSource.asObservable();
  listFriendIdConnected$ = this.listFriendIdConnectedSource.asObservable();
  friendInvitationAdded$ = this.friendInvitationAddedSource.asObservable();
  constructor(private userService: UserService) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${apiUrl}/HubPresence`, {
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
  stopConnection() {
    if (this.connection) {
      try {
        this.connection.stop();
        console.log('Đã ngắt kết nối SignalR');
      } catch (err) {
        console.error('Lỗi khi dừng kết nối: ', err);
      }
    }
  }
  async sendNotification(notification: CreateNotification) {
    return this.connection.invoke('SenNotification', notification)
      .catch(error => console.log(error));
  }
  async makeFriend(request: CreateFriendShip) {
    return this.connection.invoke('MakeFriend', request)
      .catch(error => console.log(error));
  }
  async GetFriendIdOfCurrentUser() {
    return this.connection.invoke('GetFriendIdOfCurrentUser')
      .catch(error => console.log(error));
  }
  private registerSignalREvents() {
    this.connection.on('getProfileInfo', (user) => {
      this.profileAddedSource.next(user);
    });
    this.connection.on('removeUser', (userId) => {
      this.userOnDisconnectedSource.next(userId);
    });
    this.connection.on('FriendIdOfCurrentUser', (friendIds) => {
      this.listFriendIdConnectedSource.next(friendIds);
    });
    this.connection.on('addUserConnected', (userId) => {
      this.userConnectedAddedSource.next(userId);
    });
    this.connection.on('createNotification', (notification) => {
      this.notificationAddedSource.next(notification);
    });
    this.connection.on('addFriendship', (friend) => {
      this.friendInvitationAddedSource.next(friend);
    });
  }
}
