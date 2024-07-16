import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { Account } from 'src/app/Models/Account/Account.entity';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateFriendShip } from 'src/app/Models/FriendShip/CreateFriendShip.entity';
import { FriendshipStatus } from 'src/app/Models/FriendShip/FriendShip.entity';
import { GroupMemberStatus } from 'src/app/Models/Group/AddGroupMember.entity';
import { Group } from 'src/app/Models/Group/Group.entity';
import { JoinGroup } from 'src/app/Models/Group/JoinGroup.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UserService } from 'src/app/services/User.service';
import { CommentService } from 'src/app/services/comment.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { GroupService } from 'src/app/services/group.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';
import { SignalRService } from 'src/app/services/signal-rservice.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  constructor(
    private router:Router,
    private readonly UserService:UserService,
    private readonly postService:PostService,
    private groupService:GroupService,
    private clipboard:ClipboardService,
    private readonly FriendService:FriendShipService,
    private readonly InteractionService:InteractionsService,
    private readonly CommentService:CommentService,
    private readonly signalRService:SignalRService
  ) {}
  keyword:string = "";
  Users:BaseQueriesResponse<Account> = {
    pageIndex: 1,
    pageSize:10,
    items:[],
    keyword: "",
    total: 0
  }
  CreateFriend:CreateFriendShip = {
    receiverId: "",
    senderId: "",
    status: FriendshipStatus.Pending,
    requestedAt:new Date()
  }
  Posts:BaseQueriesResponse<Post> = {
    pageIndex:1,
    pageSize:10,
    keyword:"",
    total:0,
    items:[]
  };
  Groups:BaseQueriesResponse<Group> = {
    pageIndex:1,
    pageSize:10,
    keyword:"",
    total:0,
    items:[]
  }
  CreateReact: CreateInteraction = {
    userId: "",
    postId: 0,
    emojiId: 0,
    created: new Date()
  }
  CreateComment: CreateComment = {
    userId: "",
    content: "",
    postId: 0,
    created: new Date()
  }
  showModal: boolean = false;
  postDetail:Post = {
    userId:"",
    nameUser:"",
    id:0,
    image:[],
    imageUser:"",
    comment:[],
    content:"",
    totalComment:0,
    pageIndexComment:1,
    pageSizeComment:15,
    totalReactions:0,
    isReact:false,
    created:new Date
  }
  joinGroupRequest: JoinGroup = {
    groupId:0,
    userId:"",
    groupMemberStatus:GroupMemberStatus.Pending
  }
  displayImageUser:string = "";
  ngOnInit(){
    this.loadKeyword();
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd && this.router.url.includes('/search')) {
        this.loadKeyword();
        this.LoadAccount();
        this.LoadGroup();
        this.LoadPost();
      }
    });
    this.LoadAccount();
    this.LoadPost();
    this.LoadGroup();
    this.LoadCurrentUser();
  }
  loadKeyword(){
    const queryParams = this.router.parseUrl(this.router.url).queryParams;
    this.keyword = queryParams['keyword'];
    console.log(this.keyword);
  }
  LoadAccount(){
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
  LoadPost(){
    this.Posts.keyword = this.keyword;
    this.postService.Search(this.Posts.pageIndex, this.Posts.pageSize,this.Posts.keyword).subscribe(res =>{
      this.Posts = res;
    })
  }
  LoadGroup(){
    this.Groups.keyword = this.keyword;
    this.groupService.getGroup(this.Groups.pageIndex, this.Groups.pageSize,this.Groups.keyword).subscribe(res =>{
      this.Groups = res;
    })
  }
  CreateFriendShip(receiverId:string){
    this.CreateFriend.receiverId = receiverId;
    this.FriendService.create(this.CreateFriend).subscribe(res =>{
      if(res.success == true){
        alert(res.message)
      }
    })
  }
  React(postId: number, emojiId: number): void {
    this.CreateReact.postId = postId;
    this.CreateReact.emojiId = emojiId;
    this.InteractionService.React(this.CreateReact).subscribe(
      response => {
        console.log('Tương tác thành công!', response);
      },
      error => {
        console.error('Đã có lỗi xảy ra', error);
      }
    );
  }
  sharePost(postId: number) {
    const url = `http://localhost:4200?postId=${postId}`;
    this.clipboard.copyFromContent(url);
    alert("Copy đường dẫn bài viết thành công!")
  }
  AddComment(postId: number) {
    this.CreateComment.postId = postId;
    this.CommentService.create(this.CreateComment).subscribe(res => {
      this.CreateComment.postId = 0;
      this.CreateComment.content = "";
    })
  }
  LoadMoreComment(postId: number) {
    this.Posts.items.forEach(element => {
      if (element.id === postId) {
        let page = ++element.pageIndexComment;
        this.CommentService.getPagedData(page, element.pageSizeComment, postId).subscribe(res => {
          element.comment = element.comment.concat(res.items);
        });
      }
    });
  }
  LoadPostDetail(idPost:number){
    if(idPost != null){
      this.showModal = true;
      this.signalRService.joinRoom(`Post_${idPost}`);
      this.postService.getById(idPost).subscribe(res =>{
        this.postDetail = res;
        this.postDetail.pageIndexComment = 1;
        this.postDetail.pageSizeComment = 15;
        this.CommentService.getPagedData(this.postDetail.pageIndexComment, this.postDetail.pageSizeComment, idPost).subscribe(res => {
          this.postDetail.comment = res.items;
        })
      })
    }
  }
  ClosePostDetail(){
    this.showModal = false;
    this.signalRService.LeaveRoom(`Post_${this.postDetail.id}`)
  }
  LoadCurrentUser(){
    var userId = this.UserService.getUser().id;
    this.UserService.getPersionalInfor(userId).subscribe(res =>{
      this.displayImageUser = res.image
    })
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
