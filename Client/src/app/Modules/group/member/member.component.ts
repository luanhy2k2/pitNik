import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { userImageUrl } from 'src/app/Environments/env';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { GroupMember } from 'src/app/Models/Group/GroupMember.entity';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css']
})
export class MemberComponent {
  public userImageUrl = userImageUrl;
  constructor(private readonly groupService:GroupService, private readonly route:ActivatedRoute){}
  idGroup:number = 0;
  members:BaseQueriesResponse<GroupMember> = {
    items:[],
    total:0,
    pageIndex:1,
    pageSize:20,
    keyword:""
  };
  ngOnInit(){
    this.loadMember();
  }
  loadMember(){
    this.route.queryParams.subscribe(res =>{
      this.idGroup = res['id'];
    })
    this.groupService.getMemberOfGroup(this.idGroup, this.members.pageIndex, this.members.pageSize,this.members.keyword).subscribe(res =>{
      this.members = res;
      console.log(res)
    })
  }
}
