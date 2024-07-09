import { Component } from '@angular/core';
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
  constructor(private readonly groupService:GroupService){}
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
    this.loadGroup();
  }
  loadGroup(){
    this.groupService.getGroup(this.groups.pageIndex, this.groups.pageSize,this.groups.keyword).subscribe(res =>{
      this.groups.items = res.items,
      this.groups.total = res.total,
      this.groups.keyword = res.keyword
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
}