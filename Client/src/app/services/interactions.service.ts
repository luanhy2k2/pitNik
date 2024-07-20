import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateInteraction } from '../Models/Interaction/CreateInteraction.entity';
import { UserService } from './User.service';
import { Observable } from 'rxjs';
import { BaseCommandResponse } from '../Models/Common/BaseCommandResponse.entity';
import { ReactRes } from '../Models/Interaction/ReactRes.entity';

@Injectable({
  providedIn: 'root'
})
export class InteractionsService {
  private apiUrl = "http://pitnik.somee.com";
  constructor(private readonly HttpClient:HttpClient, private readonly userService:UserService) { }
  React(react:CreateInteraction):Observable<BaseCommandResponse<ReactRes>>{
    react.userId = this.userService.getUser().id;
    return this.HttpClient.post<BaseCommandResponse<ReactRes>>(`${this.apiUrl}/api/Interaction/React`, react,{headers: this.userService.addHeaderToken()});
  }
}
