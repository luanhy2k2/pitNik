import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { commentRequest } from 'src/app/Models/Comment/commentRequest.entity';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { BasePaging } from 'src/app/Models/Paging.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UserService } from 'src/app/services/User.service';
import { CommentService } from 'src/app/services/comment.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';
import { SignalRService } from 'src/app/services/signal-rservice.service';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  constructor(
    private readonly postService: PostService,
    private readonly route:ActivatedRoute,
    private readonly InteractionService: InteractionsService,
    private readonly CommentService: CommentService,
    private readonly UserService:UserService,
    private readonly signalRService:SignalRService,
    private clipboard:ClipboardService
  ) {}
  Posts: BaseQueriesResponse<Post> = {
    pageIndex: 1,
    pageSize: 5,
    total: 0,
    items: [],
    keyword: ""
  }
  PagingPost: BasePaging = {
    pageIndex: 1,
    pageSize: 5,
  }
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
  imageSrcs: (string | ArrayBuffer | null)[] = [];
  displayImageUser:string = "";
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
  showModal: boolean = false;
  ngOnInit() {
    this.route.queryParams.subscribe(res => {
      var postId = res['postId'];
      if (postId != null) {
        this.showModal = true;
        this.LoadPostDetail(postId);
      } else {
        this.showModal = false;
      }
    });
    this.LoadPost();
    this.signalRService.commentAdded$.subscribe(res =>{
      this.postDetail.comment.push(res);
      console.log(res);
    })
    this.signalRService.reactAdded$.subscribe(res =>{
      console.log(res);
      this.LoadPost();
    })
    this.signalRService.postAdded$.subscribe(res =>{
      this.LoadPost();
    })
    this.LoadCurrentUser();
  };
  LoadPost() {
    this.postService.getPost(this.Posts.pageIndex, this.Posts.pageSize, this.Posts.keyword).subscribe(
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
    this.postService.createPost(this.CreatePost).subscribe(
      response => {
        console.log('Post created successfully', response);
        this.LoadPost(); // Refresh posts
        this.CreatePost.content = "";
        this.CreatePost.files = [];
        this.imageSrcs = [];
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
}
