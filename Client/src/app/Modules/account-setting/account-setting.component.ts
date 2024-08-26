import { Component } from '@angular/core';
import { userImageUrl } from 'src/app/Environments/env';
import { GeneralInfo, Account, Gender } from 'src/app/Models/Account/Account.entity';
import { ResetPassword } from 'src/app/Models/Account/ResetPassword';
import { UpdatePersionalInfor } from 'src/app/Models/Account/UpdateAccount.entity';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-account-setting',
  templateUrl: './account-setting.component.html',
  styleUrls: ['./account-setting.component.scss']
})
export class AccountSettingComponent {
  public userImageUrl = userImageUrl;
  constructor(private readonly UserService:UserService){}
  GeneralInfo:GeneralInfo = {
    id: 0,
    aboutMe:"",
    workAndExperience:"",
    hobbies:"",
    userId:"",
    education:""
  }
  displayImage:string = "";
  PersionalInfo:UpdatePersionalInfor = {
    id:"",
    name:"",
    userName:"",
    image: new File([""], ""),
    address:"",
    phoneNumber:"",
    email:"",
    birthDay:new Date(),
    gender:Gender.Male
  }
  resetPasswordReq:ResetPassword = {
    userId:"",
    oldPassword:"",
    newPassword:"",
    confirmPassword:""
  };
  ngOnInit(){
    this.LoadGeneralInfo();
    this.LoadPersionalInfo();
  }
  onFileSelected(event: any) {
    this.PersionalInfo.image = event.target.files[0];
  }
  ResetPassword(){
    this.resetPasswordReq.userId = this.UserService.getUser().id;
    this.UserService.ResetPassword(this.resetPasswordReq).subscribe(res =>{
      if(res.success == true){
        this.resetPasswordReq.oldPassword = "";
        this.resetPasswordReq.newPassword = "";
        this.resetPasswordReq.confirmPassword = "";
      }
      alert(res.message)
    })
  }
  LoadGeneralInfo(){
    var userId = this.UserService.getUser().id;
    this.UserService.getGeneralInfor(userId).subscribe(res =>{
      this.GeneralInfo = res;
      this.GeneralInfo.userId = userId
    })
  }
  LoadPersionalInfo(){
    var userId = this.UserService.getUser().id;
    this.UserService.getPersionalInfor(userId).subscribe(res =>{
      this.PersionalInfo.id = res.id,
      this.PersionalInfo.name = res.name,
      this.PersionalInfo.birthDay = res.birthday,
      this.PersionalInfo.address = res.address,
      this.PersionalInfo.phoneNumber = res.phoneNumber,
      this.PersionalInfo.email = res.email,
      this.PersionalInfo.userName = res.userName,
      this.displayImage = res.image
    })
  }
  UpdatePersionalInfor(){
    this.UserService.updatePersionalInfor(this.PersionalInfo).subscribe(res =>{
      if(res.success == true){
        alert("Cập nhật thông tin thành công!");
      }
    })
  }
  UpdateGeneralInfor(){
    this.UserService.updateGeneralInfor(this.GeneralInfo).subscribe(res =>{
      if(res.success == true){
        alert("Cập nhật thông tin thành công!");
      }
    })
  }
}
