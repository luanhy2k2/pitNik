import { Component } from '@angular/core';
import { groupImageUrl } from 'src/app/Environments/env';
import { GeneralInfo } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Group } from 'src/app/Models/Group/Group.entity';
import { UserService } from 'src/app/services/User.service';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-sidebar-right',
  templateUrl: './sidebar-right.component.html',
  styleUrls: ['./sidebar-right.component.css']
})
export class SidebarRightComponent {
  public groupImageUrl = groupImageUrl;
  constructor(private readonly UserService:UserService,private readonly groupService:GroupService){}
  GeneralInfo:GeneralInfo = {
    aboutMe: "",
    workAndExperience:"",
    hobbies:"",
    id:0,
    userId:"",
    education:""
  }
  groups:BaseQueriesResponse<Group> ={
    items:[],
    pageIndex:1,
    pageSize:15,
    keyword:"",
    total:0
  }
  userId:string = "";
  ngOnInit(){
    this.LoadGeneralInfo();
    this.LoadMyGroup();
  }
  LoadGeneralInfo(){
    this.userId = this.UserService.getUser().id;
    this.UserService.getGeneralInfor(this.userId).subscribe(res =>{
      this.GeneralInfo = res;
    })
  }
  LoadMyGroup(){
    this.groupService.getMyGroup(this.groups.pageIndex, this.groups.pageSize,this.groups.keyword).subscribe(res =>{
      this.groups = res;
    })
  }
}
