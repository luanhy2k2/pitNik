import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { postImageUrl, userImageUrl } from 'src/app/Environments/env';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { BasePaging } from 'src/app/Models/Paging.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UpdatePost } from 'src/app/Models/Post/update-post';
import { UserService } from 'src/app/services/User.service';
import { CommentService } from 'src/app/services/comment.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';


@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent {
  public postImageUrl = postImageUrl;
  public userImageUrl = userImageUrl;
  constructor(
    private readonly postService: PostService,
    private readonly route:ActivatedRoute,
    private readonly InteractionService: InteractionsService,
    private readonly CommentService: CommentService,
    private readonly UserService:UserService,
    private readonly commentService:CommentService,
    private readonly friendService:FriendShipService,
    private clipboard:ClipboardService
  ) {}
  Posts: BaseQueriesResponse<Post> = {
    pageIndex: 1,
    pageSize: 5,
    total: 0,
    items: [],
    keyword: ""
  }
  userId:string = "";
  CreatePost: CreatePost = {
    userId: '',
    content: '',
    files: [],
    groupId:0,
    id: 0,
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
  UpdatePostReq:UpdatePost = {
    id:0,
    imageNameDelete:[],
    content:"",
    files:[],
    images:[]
  }
  imageSrcs: (string | ArrayBuffer | null)[] = [];
  currentUser:any = {};
  postDetail:Post = {
    userId:"",
    nameUser:"",
    id:0,
    image:[],
    imageUser:"",
    comment:[],
    content:"",
    groupId:0,
    totalComment:0,
    pageIndexComment:1,
    pageSizeComment:15,
    totalReactions:0,
    isReact:false,
    created:new Date
  }
  showModal: boolean = false;
  showModalEdit:boolean = false;
  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.userId = params['id'] || this.UserService.getUser().id;
      this.friendService.userId = this.userId
    });
    this.LoadPost();
    this.LoadCurrentUser();
  };
  LoadPost() {
    this.postService.getPostOfUser(this.userId,this.Posts.pageIndex, this.Posts.pageSize, this.Posts.keyword).subscribe(
      res => {
        this.Posts.items = res.items;
        this.Posts.total = res.total;
      },
      err => {
        console.log(err);
      }
    );
  }

  LoadPostDetail(idPost:number){
    if(idPost != null){
      this.showModal = true;
      this.commentService.joinPost(`Post_${idPost}`);
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
  openEditModal(idPost:number){
    if(idPost != null){
      this.showModalEdit = true;
      this.postService.getById(idPost).subscribe(res =>{
        this.UpdatePostReq.content = res.content;
        this.UpdatePostReq.id = idPost;
        this.UpdatePostReq.images = res.image,
        console.log(this.UpdatePostReq)
        this.CommentService.getPagedData(this.postDetail.pageIndexComment, this.postDetail.pageSizeComment, idPost).subscribe(res => {
          this.postDetail.comment = res.items;
        })
      })
    }
  }
  closeEditModel(){
    this.UpdatePostReq.content = "";
    this.UpdatePostReq.id = 0;
    this.UpdatePostReq.images = [];
    this.UpdatePostReq.imageNameDelete = [];
    this.imageSrcs = [];
    this.showModalEdit = false;
  }
  ClosePostDetail(){
    for(let i = 0; i<this.Posts.items.length; i++){
      if(this.Posts.items[i].id == this.postDetail.id){
        this.Posts.items[i] = this.postDetail;
        break;
      }
    }
    this.showModal = false;
    this.commentService.LeavePost(`Post_${this.postDetail.id}`)
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
      this.currentUser.image = res.image;
      this.currentUser.id = res.id;
      this.currentUser.name = res.name;
    })
  }
  AddComment(postId: number) {
    this.CreateComment.postId = postId;
    this.CommentService.create(this.CreateComment).subscribe(res => {
      this.CreateComment.postId = 0;
      this.CreateComment.content = "";
    })
  }
  removeImagePost(name:string){
    const index = this.UpdatePostReq.images.indexOf(name);
    if (index !== -1) {
      this.UpdatePostReq.images.splice(index, 1);
      this.UpdatePostReq.imageNameDelete.push(name);
    }
  }
  onFilesSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files) {
      Array.from(fileInput.files).forEach(file => {
        if(this.showModalEdit == true){
          this.UpdatePostReq.files.push(file);
        }
        else{
          this.CreatePost.files.push(file); // Update CreatePost object
        }
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
  updatePost(){
    this.postService.updatePost(this.UpdatePostReq).subscribe(res =>{
      if(res.success == true){
        for (let index = 0; index < this.Posts.items.length; index++) {
          if(this.Posts.items[index].id == this.UpdatePostReq.id){
            this.Posts.items[index].content = res.object.content;
            this.Posts.items[index].image = res.object.image;
            break;
          }
        }
        alert(res.message);
      }
      else{
        alert(res.message);
      }
    })
  }
  AddPost(): void {
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
                break;
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
  sharePost(postId: number) {
    const url = `http://localhost:4200?postId=${postId}`;
    this.clipboard.copyFromContent(url);
    alert("Copy đường dẫn bài viết thành công!")
  }
}
