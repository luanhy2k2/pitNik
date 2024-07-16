import { Component } from '@angular/core';
import { Register } from 'src/app/Models/Account/Register.entity';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private readonly userService:UserService){}
  registerReq:Register = {
    name:"",
    address:"",
    phoneNumber:"",
    password:"",
    userName:"",
    email:"",
    gender:0,
    birthday:new Date,
    image:"",
    confirmPassword:""
  }
  Register(){
    this.userService.register(this.registerReq).subscribe(res =>{
      if(res.success == true){
        this.userService.GenerateTokenConfirmEmail(this.registerReq.email).subscribe(res =>{
          if(res == true){
            alert("Vui lòng vào gmail để xác thực tài khoản")
          }
        })
      }
    })
  }
  
}
