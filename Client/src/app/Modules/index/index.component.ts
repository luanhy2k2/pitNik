import { Component } from '@angular/core';
import { Comment, defaultCommentQuery } from 'src/app/Models/Comment/comment.entity';
import { commentRequest } from 'src/app/Models/Comment/commentRequest.entity';
import { CreateComment } from 'src/app/Models/Comment/create-comment.entity';
import { BaseQueriesResponse } from 'src/app/Models/Common/BaseQueriesResponse.entity';
import { CreateInteraction } from 'src/app/Models/Interaction/CreateInteraction.entity';
import { BasePaging } from 'src/app/Models/Paging.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { Post } from 'src/app/Models/Post/Post.entity';
import { UserService } from 'src/app/services/User.service';
import { ChatHubService } from 'src/app/services/chatHub.service';
import { CommentService } from 'src/app/services/comment.service';
import { InteractionsService } from 'src/app/services/interactions.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  constructor(
    private readonly postService: PostService,
    private readonly chatHubService: ChatHubService,
    private readonly InteractionService: InteractionsService,
    private readonly CommentService: CommentService
  ) { }
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
    id: 0,
    created: new Date()
  };
  CreateReact: CreateInteraction = {
    userId: "",
    postId: 0,
    emojiId: 0,
    created: new Date()
  }
  PagingComment: commentRequest = {
    pageIndex: 1,
    pageSize: 10,
    postId: 0
  }
  Comments: Comment[] = []
  CreateComment: CreateComment = {
    userId: "",
    content: "",
    postId: 0,
    created: new Date()
  }
  imageSrcs: (string | ArrayBuffer | null)[] = [];

  ngOnInit() {
    this.LoadPost();
    this.chatHubService.startConnection().then(() => {
      this.chatHubService.addPostListener((post: any) => {
        this.LoadPost();
      });
      this.chatHubService.addReactListener((react: any) => {
        this.LoadPost();
      });
      this.chatHubService.addCommentLister((comment: Comment) => {
        this.Comments.push(comment);
      });
    });
  }
  LoadPost() {
    this.postService.getPagedData(this.Posts.pageIndex, this.Posts.pageSize, this.Posts.keyword).subscribe(
      res => {
        this.Posts.items = res.items;
        this.Posts.total = res.total;
        console.log(this.Posts.items)
      },
      err => {
        console.log(err);
      }
    );
  }
  LoadComment(postId: number) {
    this.Posts.items.forEach(element => {
      if (element.id == postId) {
        element.pageIndexComment = 1;
        element.pageSizeComment = 10;
        this.CommentService.getPagedData(element.pageIndexComment, element.pageSizeComment, postId).subscribe(res => {
          element.comment = res.items;
          console.log(this.Comments)
        })
      }
    });
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

  AddComment(postId: number) {
    this.CreateComment.postId = postId;
    this.CommentService.create(this.CreateComment).subscribe(res => {
      this.LoadComment(postId)
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
        this.LoadPost(); // Refresh posts
      },
      error => {
        console.error('Đã có lỗi xảy ra', error);
      }
    );
  }
}
