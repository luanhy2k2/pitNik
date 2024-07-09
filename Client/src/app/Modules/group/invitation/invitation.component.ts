import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { GroupMemberStatus } from 'src/app/Models/Group/AddGroupMember.entity';
import { Invitation } from 'src/app/Models/Group/JoinGroup.entity';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent {
  constructor(private readonly groupService:GroupService, private readonly route:ActivatedRoute){}
  invitations:BaseQueriesResponse<Invitation> = {
    items:[],
    total:0,
    pageIndex:1,
    pageSize:10,
    keyword:""
  };
  groupId:number = 0
  ngOnInit(){
    this.loadInvitation();
  }
  loadInvitation(){
    this.route.queryParams.subscribe(res =>{
      this.groupId = res['id'];
    })
    this.groupService.getInvitation(this.groupId, this.invitations.pageIndex,this.invitations.pageSize,this.invitations.keyword).subscribe(
      res =>{
        this.invitations = res
      }
    )
  }
  updateStatusInvitation(invitationId:number, status:GroupMemberStatus){
    this.groupService.UpdateStatusInvitation(invitationId,status).subscribe(res =>{
      if(res.success == true){
        if(status == GroupMemberStatus.Accepted){
          alert("Chấp nhận lời mời tham gia thành công!")
        }
        else if(status == GroupMemberStatus.Rejected){
          alert("Từ chối lời mời tham gia thành công!")
        }
        this.loadInvitation();
      }
    })
  }
}
