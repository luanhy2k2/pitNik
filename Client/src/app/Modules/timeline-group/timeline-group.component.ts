import { Component } from '@angular/core';
import { groupImageUrl } from 'src/app/Environments/env';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { GroupMemberStatus } from 'src/app/Models/Group/AddGroupMember.entity';
import { CreateGroup } from 'src/app/Models/Group/CreateGroup.entity';
import { Group } from 'src/app/Models/Group/Group.entity';
import { JoinGroup } from 'src/app/Models/Group/JoinGroup.entity';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-timeline-group',
  templateUrl: './timeline-group.component.html',
  styleUrls: ['./timeline-group.component.css']
})
export class TimelineGroupComponent {
  public groupImageUrl = groupImageUrl;
  constructor(private readonly groupService:GroupService){}
  myGroups:BaseQueriesResponse<Group> = {
    pageIndex: 1,
    pageSize:10,
    keyword:"",
    total:0,
    items:[]
  };
  groups:BaseQueriesResponse<Group> = {
    pageIndex: 1,
    pageSize:10,
    keyword:"",
    total:0,
    items:[]
  };
  createGroupRequest:CreateGroup = {
    name:"",
    description:"",
    background:new File([""], "")
  };
  joinGroupRequest: JoinGroup = {
    groupId:0,
    userId:"",
    groupMemberStatus:GroupMemberStatus.Pending
  }
  ngOnInit(){
    this.loadMyGroup();
    this.loadGroup();
  }
  loadMyGroup(){
    this.groupService.getMyGroup(this.groups.pageIndex, this.groups.pageSize,this.groups.keyword).subscribe(res =>{
      this.myGroups = res
    })
  }
  loadGroup(){
    this.groupService.getGroup(this.groups.pageIndex, this.groups.pageSize,this.groups.keyword).subscribe(res =>{
      this.groups = res
    })
  }
  backgroundSrcs: (string | ArrayBuffer | null) = null;
  onFileChange(event: any) {
    const file = event.target.files[0];
    this.createGroupRequest.background = file;
    if (file.type.startsWith('image/')) {
      const reader = new FileReader();
      reader.onload = (e: ProgressEvent<FileReader>) => {
        if (e.target) {
          this.backgroundSrcs = e.target.result;
        }
      };
      reader.readAsDataURL(file);
    }
  }
  CreateGroup(){
    this.groupService.create(this.createGroupRequest).subscribe(
      res =>{
        if(res.success == true){
          alert("Tạo nhóm thành công!");
          this.groups.items.unshift(res.object);
          this.myGroups.items.unshift(res.object)
        }  
      }
    )
  }
  JoinGroup(id:number){
    this.joinGroupRequest.groupId = id;
    this.groupService.JoinGroup(this.joinGroupRequest).subscribe(res =>{
      if(res.success == true){
        alert("Gửi yêu cầu tham gia thành công!");
      }
    })
  }
  isPopupActive: boolean = false;
  openPopup() {
    this.isPopupActive = true;
  }
  closePopup() {
    this.isPopupActive = false;
  }
}
