import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { Group } from 'src/app/Models/Group/Group.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UserService } from 'src/app/services/User.service';
import { CommentService } from 'src/app/services/comment.service';
import { GroupService } from 'src/app/services/group.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';
import { SignalRService } from 'src/app/services/signal-rservice.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss']
})
export class GroupComponent {
  constructor(
     private readonly groupService:GroupService,
     private readonly postService:PostService, 
     private readonly signalRService:SignalRService,
     private readonly InteractionService:InteractionsService,
     private readonly CommentService:CommentService,
     private readonly UserService:UserService,
     private route:ActivatedRoute){}
  idGroup:number = 0;
  group:Group = {
    id:0,
    name:"",
    description:"",
    created:new Date,
    isJoined:false,
    background:"",
    totalMember:0
  };
  Posts: BaseQueriesResponse<Post> = {
    pageIndex: 1,
    pageSize: 5,
    total: 0,
    items: [],
    keyword: ""
  }
  CreatePost: CreatePost = {
    userId: '',
    content: '',
    files: [],
    id: 0,
    groupId: 0,
    created: new Date()
  };
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
  postDetail:Post = {
    userId:"",
    nameUser:"",
    id:0,
    groupId:0,
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
  displayImageUser:string = "";
  showModal: boolean = false;
  imageSrcs: (string | ArrayBuffer | null)[] = [];
  ngOnInit(){
    this.route.queryParams.subscribe(params => {
      this.idGroup = params['id'];
    })
    this.signalRService.commentAdded$.subscribe(res =>{
      this.postDetail.comment.push(res);
      console.log(res);
    })
    this.signalRService.reactAdded$.subscribe(res =>{
      console.log(res);
      this.loadPost();
    })
    this.signalRService.postAdded$.subscribe(res =>{
      this.loadPost();
    })
    this.loadGroupData();
    this.loadPost();
    this.LoadCurrentUser();
  }
  loadGroupData(){
    this.groupService.getGroupDetail(this.idGroup).subscribe(res =>{
      this.group = res;
    })
  }
  loadPost(){
    this.postService.getPostOfGroup(this.idGroup,this.Posts.pageIndex,this.Posts.pageSize,this.Posts.keyword).subscribe(res =>{
      this.Posts = res;
    })
  }
  LoadPostDetail(idPost:number){
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
  ClosePostDetail(){
    for(let i = 0; i<this.Posts.items.length; i++){
      if(this.Posts.items[i].id == this.postDetail.id){
        this.Posts.items[i] = this.postDetail;
        break;
      }
    }
    this.showModal = false;
    this.signalRService.LeaveRoom(`Post_${this.postDetail.id}`)
  }
  AddComment(postId: number) {
    this.CreateComment.postId = postId;
    this.CommentService.create(this.CreateComment).subscribe(res => {
      this.CreateComment.postId = 0;
      this.CreateComment.content = "";
    })
  }
  onFilesSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files) {
      Array.from(fileInput.files).forEach(file => {
        this.CreatePost.files.push(file); // Update CreatePost object
        if (file.type.startsWith('image/')) {
          const reader = new FileReader();
          reader.onload = (e: ProgressEvent<FileReader>) => {
            if (e.target) {
              this.imageSrcs.push(e.target.result);
            }
          };
          reader.readAsDataURL(file);
        }
      });
    }
    console.log(this.imageSrcs);
  }
  AddPost(): void {
    this.CreatePost.groupId = this.idGroup;
    this.postService.createPost(this.CreatePost).subscribe(
      response => {
        if(response.success == true){
          this.Posts.items.unshift(response.object);
          this.CreatePost.content = "";
          this.CreatePost.files = [];
          this.imageSrcs = [];
        }
        if(response.success == false){
          alert(response.message);
        }
      },
      error => {
        console.error('Error creating post', error);
      }
    );
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
  LoadCurrentUser(){
    var userId = this.UserService.getUser().id;
    this.UserService.getPersionalInfor(userId).subscribe(res =>{
      this.displayImageUser = res.image
    })
  }
  React(postId: number, emojiId: number): void {
    this.CreateReact.postId = postId;
    this.CreateReact.emojiId = emojiId;
    this.InteractionService.React(this.CreateReact).subscribe(
      response => {
        if(response.success == true){
          if(this.postDetail.id != postId){
            for(let post of this.Posts.items){
              if(post.id == postId){
                if(response.object.isReact == false){
                  post.isReact = false
                  post.totalReactions--;
                }
                else{
                  post.isReact = true;
                  post.totalReactions++ 
                }
              }
            }
          }
        }
      },
      error => {
        console.error('Đã có lỗi xảy ra', error);
      }
    );
  }
}
