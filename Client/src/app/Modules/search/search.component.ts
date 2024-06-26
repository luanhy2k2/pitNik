import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from 'src/app/Models/Account/Account.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { UserService } from 'src/app/services/User.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  constructor(
    private readonly route:ActivatedRoute,
    private readonly UserService:UserService
  ) {}
  keyword:string = "";
  Users:BaseQueriesResponse<Account> = {
    pageIndex: 1,
    pageSize:10,
    items:[],
    keyword: "",
    total: 0
  }
  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      this.keyword = params['keyword'];
    })
  }
  LoadAccount(){
    console.log(this.keyword)
    this.Users.keyword = this.keyword;
    this.UserService.getPagedData(this.Users.pageIndex,this.Users.pageSize, this.Users.keyword).subscribe(
      res =>{
        this.Users.items = res.items,
        this.Users.pageIndex = res.pageIndex,
        this.Users.pageSize = res.pageSize,
        this.Users.total = res.total,
        this.Users.keyword = res.keyword
      }
    )
  }
}
