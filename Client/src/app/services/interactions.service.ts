import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateInteraction } from '../Models/Interaction/CreateInteraction.entity';
import { UserService } from './User.service';

@Injectable({
  providedIn: 'root'
})
export class InteractionsService {
  private apiUrl = "https://localhost:7261";
  constructor(private readonly HttpClient:HttpClient, private readonly userService:UserService) { }
  React(react:CreateInteraction){
    react.userId = this.userService.getUser().id;
    return this.HttpClient.post(`${this.apiUrl}/api/Interaction/React`, react,{headers: this.userService.addHeaderToken()});
  }
}
