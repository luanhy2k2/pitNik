import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { groupImageUrl } from 'src/app/Environments/env';
import { Group } from 'src/app/Models/Group/Group.entity';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-group-profile',
  templateUrl: './group-profile.component.html',
  styleUrls: ['./group-profile.component.css']
})
export class GroupProfileComponent {
  public groupImageUrl = groupImageUrl;
  constructor(private readonly groupService:GroupService, private route:ActivatedRoute){}
  idGroup:number = 0;
  group:Group = {
    id:0,
    name:"",
    description:"",
    created:new Date,
    isJoined:false,
    background:"",
    totalMember:0,
    creatorId:""
  }
  ngOnInit(){
    this.loadGroupData();
  }
  loadGroupData(){
    this.route.queryParams.subscribe(params => {
      this.idGroup = params['id'];
    })
    this.groupService.getGroupDetail(this.idGroup).subscribe(res =>{
      this.group = res;
    })
  }
}
