import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/User.service';
import { ChatHubService } from 'src/app/services/chatHub.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private readonly UserService:UserService, private readonly route:Router){}
  userName:string = "";
  passWord:string = "";
  Login(){
    this.UserService.login(this.userName,this.passWord).subscribe(
      res =>{
        alert("Đăng nhập thành công!");
        localStorage.setItem("user", JSON.stringify(res));
        this.route.navigate(['/']);
      },
      err =>{
        alert(err);
      }
    )
  }
}
