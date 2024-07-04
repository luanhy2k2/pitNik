import { Component } from '@angular/core';
import { Account } from 'src/app/Models/Account/Account.entity';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent {
  constructor(private readonly userService:UserService){}
  displayImage:string = "";
  ngOnInit(){
    this.loadUserInfo();
  }
  loadUserInfo(){
    var userId = this.userService.getUser().id;
    this.userService.getPersionalInfor(userId).subscribe(res =>{
      this.displayImage = res.image;
    })
  }
}
