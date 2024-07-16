import { Component } from '@angular/core';
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
  ngOnInit(){
    this.LoadGeneralInfo();
    this.LoadMyGroup();
  }
  LoadGeneralInfo(){
    this.UserService.getGeneralInfor(this.UserService.getUser().id).subscribe(res =>{
      this.GeneralInfo = res;
    })
  }
  LoadMyGroup(){
    this.groupService.getMyGroup(this.groups.pageIndex, this.groups.pageSize,this.groups.keyword).subscribe(res =>{
      this.groups = res;
    })
  }
}
