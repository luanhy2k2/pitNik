import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard';
import { postImageUrl, userImageUrl } from 'src/app/Environments/env';
import { commentRequest } from 'src/app/Models/Comment/commentRequest.entity';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { CreateReplyComment } from 'src/app/Models/Comment/create-reply-comment';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { CreateNotification } from 'src/app/Models/Notification/CreateNotification';
import { Notification } from 'src/app/Models/Notification/Notification.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UserService } from 'src/app/services/User.service';
import { CommentService } from 'src/app/services/comment.service';
import { FriendShipService } from 'src/app/services/friend-ship.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';
import { PresenceService } from 'src/app/services/presence.service';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  public postImageUrl = postImageUrl;
  public userImageUrl = userImageUrl
  constructor(
    private readonly postService: PostService,
    private readonly route: ActivatedRoute,
    private readonly InteractionService: InteractionsService,
    private readonly CommentService: CommentService,
    private readonly UserService: UserService,
    private readonly friendShipService: FriendShipService,
    private readonly presenceService: PresenceService,
    private clipboard: ClipboardService
  ) { }
  Posts: BaseQueriesResponse<Post> = {
    pageIndex: 1,
    pageSize: 10,
    total: 0,
    items: [],
    keyword: ""
  }
  CreatePostReq: CreatePost = {
    userId: '',
    content: '',
    files: [],
    groupId: 0,
    id: 0,
    created: new Date()
  };
  CreateReactReq: CreateInteraction = {
    userId: "",
    postId: 0,
    emojiId: 0,
    created: new Date()
  };
  CreateCommentReq: CreateComment = {
    userId: "",
    content: "",
    postId: 0,
    created: new Date()
  };
  uploadFileCommentReq:File[] = [];
  CreateReplyCommentReq: CreateReplyComment[] = [];
  imageSrcs: (string | ArrayBuffer | null)[] = [];
  displayImageUser: string = "";
  postDetail: Post = {
    userId: "",
    nameUser: "",
    id: 0,
    image: [],
    imageUser: "",
    comment: [],
    content: "",
    groupId: 0,
    totalComment: 0,
    pageIndexComment: 1,
    pageSizeComment: 15,
    totalReactions: 0,
    isReact: false,
    created: new Date
  }
  createNotificationReq:CreateNotification = {
    receiverId:"",
    content:"",
    postId:0,
  }
  showModal: boolean = false;
  ngOnInit() {
    this.CommentService.startConnection();
    this.route.queryParams.subscribe(res => {
      var postId = res['postId'];
      if (postId != null) {
        this.showModal = true;
        this.LoadPostDetail(postId);
      } else {
        this.showModal = false;
      }
    });
    this.friendShipService.userId = this.UserService.getUser().id;
    this.LoadPost();
    this.CommentService.commentAdded$.subscribe(res => {
      this.postDetail.comment.push(res);
      this.postDetail.totalComment++;
    })
    this.CommentService.replyCommentAdded$.subscribe(res => {
      for(let i = 0; i<=this.postDetail.comment.length; i++){
        if(this.postDetail.comment[i].id == res.commentId){
          this.postDetail.comment[i].Reply.push(res);
          break;
        }
      }
      this.postDetail.totalComment++;
    })
    this.LoadCurrentUser();
  };
  LoadPost() {
    this.postService.getPost(this.Posts.pageIndex, this.Posts.pageSize, this.Posts.keyword).subscribe(
      res => {
        this.Posts = res;
      },
      err => {
        console.log(err);
      }
    );
  }
  addEmoji(emoji: string) {
    this.CreateCommentReq.content += emoji;
  }
  addEmojiReplycomment(emoji:string, commentId:number){
 
    var textarea = document.getElementById(`contentReply_${commentId}`) as HTMLTextAreaElement;
        if (textarea !== null && textarea !== undefined){
          textarea.value += emoji;
        }
  }
  LoadPostDetail(idPost: number) {
    if (idPost != null) {
      this.showModal = true;
      this.CommentService.joinPost(`Post_${idPost}`);
      this.postService.getById(idPost).subscribe(res => {
        this.postDetail = res;
        this.postDetail.pageIndexComment = 1;
        this.postDetail.pageSizeComment = 15;
        this.CommentService.getPagedData(this.postDetail.pageIndexComment, this.postDetail.pageSizeComment, idPost).subscribe(res => {
          this.postDetail.comment = res.items;
        })
      })
    }
  }
  ClosePostDetail() {
    for (let i = 0; i < this.Posts.items.length; i++) {
      if (this.Posts.items[i].id == this.postDetail.id) {
        this.Posts.items[i] = this.postDetail;
        break;
      }
    }
    this.showModal = false;
    this.CreateReplyCommentReq = [];
    this.uploadFileCommentReq = [];
    this.imageSrcs = [];
    this.CommentService.LeavePost(`Post_${this.postDetail.id}`)
  }
  toggleEmojiReplyComment(commentId:number){
    var emojiModal = document.getElementById(`emojiReply_${commentId}`)
    if (emojiModal !== null && emojiModal !== undefined) {
      emojiModal.style.display = "block";
    }
  }
  loadReplyComment(commentId: number) {
    for (let i = 0; i < this.postDetail.comment.length; i++) {
      if (this.postDetail.comment[i].id == commentId) {
        if (this.postDetail.comment[i].PageIndexReplyComment) {
          this.postDetail.comment[i].PageIndexReplyComment++;
          if (this.postDetail.comment[i].PageIndexReplyComment > this.postDetail.comment[i].totalPageReply) {
            var load = document.getElementById(`commentId_${commentId}`);
            if (load !== null && load !== undefined) {
              load.style.display = "none";
            }
            break;
          }
        } else {
          this.postDetail.comment[i].PageIndexReplyComment = 1;
        }
        this.CommentService.getReplyComment(this.postDetail.comment[i].PageIndexReplyComment, commentId).subscribe(res => {
          if (this.postDetail.comment[i].Reply) {
            this.postDetail.comment[i].Reply = this.postDetail.comment[i].Reply.concat(res.items);
          }
          else
            this.postDetail.comment[i].Reply = res.items;
          this.postDetail.comment[i].totalPageReply = Math.ceil(res.total / 1);
        });
        break;
      }
    }
  }
  openInputReplyComment(commentId:number, commenterid:string,commentername:string){
    var existReq = this.CreateReplyCommentReq.find(e =>e.commentId == commentId);
    if(existReq){
      existReq.commenterId = commenterid;
    }
    else{
      const req: CreateReplyComment = {
        commentId: commentId,
        commenterId: commenterid,
        content:""
      }
      this.CreateReplyCommentReq.push(req);
    }
    var div = document.getElementById(`formReply_${commentId}`);
    if (div !== null && div !== undefined) {
      div.style.display = "block";
      var textarea = document.getElementById(`contentReply_${commentId}`)  as HTMLTextAreaElement;
      if (textarea !== null && textarea !== undefined){
        textarea.placeholder  = "Phản hồi bình luận của" + commentername
      }
    }
  }
  AddReplyComment(commentId:number){
    var textarea = document.getElementById(`contentReply_${commentId}`) as HTMLTextAreaElement;
      if (textarea !== null && textarea !== undefined){
        for(let item of this.CreateReplyCommentReq){
          if(item.commentId == commentId){
            item.content = textarea.value;
            this.CommentService.sendReplyComment(item).then(res =>{
              item.content = "";
              textarea.value = "";
              this.createNotificationReq.content = "Đã phản hồi bình luận của bạn";
              this.createNotificationReq.receiverId = item.commenterId;
              this.createNotificationReq.postId = this.postDetail.id;
              this.presenceService.sendNotification(this.createNotificationReq);
            })
          }
        }
      }
      
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
  LoadCurrentUser() {
    var userId = this.UserService.getUser().id;
    this.UserService.getPersionalInfor(userId).subscribe(res => {
      this.displayImageUser = res.image
    })
  }
  AddComment() {
    this.CreateCommentReq.postId = this.postDetail.id;
    if(this.uploadFileCommentReq.length > 0){
      this.CommentService.UploadFile(this.uploadFileCommentReq).subscribe(res =>{
        this.CreateCommentReq.content += res.object;
        this.CreateComment();
      })
    }
    else{
      this.CreateComment();
    }
  }
  CreateComment(){
    this.CommentService.sendComment(this.CreateCommentReq).then(res => {
      this.CreateCommentReq.postId = 0;
      this.CreateCommentReq.content = "";
      if(this.postDetail.userId != this.UserService.getUser().id){
        this.createNotificationReq.content = "Đã bình luận bài viết của bạn";
        this.createNotificationReq.receiverId = this.postDetail.userId;
        this.createNotificationReq.postId = this.postDetail.id;
        this.presenceService.sendNotification(this.createNotificationReq);
      } 
    })
  }
  onFilesSelected(event: Event, isPostFile: boolean): void {
    const fileInput = event.target as HTMLInputElement;
    if (!fileInput.files) {
      return;
    }
    Array.from(fileInput.files).forEach(file => {
      if (!file.type.startsWith('image/')) {
        return;
      }
      const reader = new FileReader();
      reader.onload = (e: ProgressEvent<FileReader>) => {
        if (e.target && e.target.result) {
          this.imageSrcs.push(e.target.result);
          if (isPostFile) {
            this.CreatePostReq.files.push(file); // Cập nhật đối tượng CreatePost
          } else {
            this.uploadFileCommentReq.push(file); // Cập nhật đối tượng uploadFileCommentReq
          }
          
          console.log("srcImg:", this.imageSrcs);
        }
      };
  
      reader.readAsDataURL(file);
    });
  } 
  removeFile(index: number, isPostFile: boolean): void {
    this.imageSrcs.splice(index, 1); // Xóa hình ảnh từ mảng imageSrcs
    if (isPostFile) {
      this.CreatePostReq.files.splice(index, 1); // Xóa tệp từ CreatePostReq
    } else {
      this.uploadFileCommentReq.splice(index, 1); // Xóa tệp từ uploadFileCommentReq
    }
  }
  
  AddPost(): void {
    this.postService.createPost(this.CreatePostReq).subscribe(
      response => {
        if (response.success == true) {
          this.Posts.items.unshift(response.object);
          this.CreatePostReq.content = "";
          this.CreatePostReq.files = [];
          this.imageSrcs = [];
          this.presenceService.GetFriendIdOfCurrentUser().then(res =>{
            res.forEach((frienId:string) =>{
              var notification:CreateNotification = {
                content: "Vừa đăng bài viết",
                receiverId: frienId,
                postId: response.object.id
              }
              this.presenceService.sendNotification(notification)
            })
          })
        }
        alert(response.message);
      },
      error => {
        console.error('Error creating post', error);
      }
    );
  }
  React(postId: number, emojiId: number): void {
    this.CreateReactReq.postId = postId;
    this.CreateReactReq.emojiId = emojiId;
    this.InteractionService.React(this.CreateReactReq).subscribe(
      response => {
        // if (response.success == true) {
        //   if (this.postDetail.id != postId) {
        //     for (let post of this.Posts.items) {
        //       if (post.id == postId) {
        //         if (response.object.isReact == false) {
        //           post.isReact = false
        //           post.totalReactions--;
        //         }
        //         else {
        //           post.isReact = true;
        //           post.totalReactions++
        //         } 
        //         break;
        //       }
        //     }
        //   }
        // }
        if (response.success == true) {
          for (let post of this.Posts.items) {
            if (post.id == postId) {
              if (response.object.isReact == false) {
                post.isReact = false
                post.totalReactions--;
              }
              else {
                post.isReact = true;
                post.totalReactions++
              } 
              if(post.userId != this.UserService.getUser().id){
                var notification:CreateNotification = {
                  receiverId: post.userId,
                  content:"Vừa tuong tác với bài viết của bạn",
                  postId: post.id
                };
                this.presenceService.sendNotification(notification);
              } 
              break;
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
