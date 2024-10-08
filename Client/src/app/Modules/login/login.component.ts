import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/User.service';
import { PresenceService } from 'src/app/services/presence.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private readonly UserService:UserService, private readonly route:ActivatedRoute
  ){}
  userName:string = "";
  passWord:string = "";
  Login(){
    this.UserService.login(this.userName,this.passWord).subscribe(
      res =>{
        alert("Đăng nhập thành công!");
        localStorage.setItem('user', JSON.stringify(res));
        window.location.href = "/";
      },
      err =>{
        alert(err);
      }
    )
  }
}
