import { Component } from '@angular/core';
import { BasePaging } from 'src/app/Models/Paging.entity';
import { CreatePost } from 'src/app/Models/Post/CreatePost.entity';
import { UserService } from 'src/app/services/User.service';
import { ChatHubService } from 'src/app/services/chatHub.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  constructor(private readonly postService: PostService, private readonly chatHubService:ChatHubService, private readonly UserService:UserService) { }
  Posts: any;
  Paging: BasePaging = {
    pageIndex: 1,
    pageSize: 5,
  }
  CreatePost: CreatePost = {
    userId: '',
    content: '',
    files: [],
    id: 0,
    created:new Date()
  };
  messageContent: string = '';
  imageSrcs: (string | ArrayBuffer | null)[] = [];

  ngOnInit() {
    this.LoadPost();
    this.chatHubService.startConnection().then(() => {
      this.chatHubService.addPostListener((post: any) => {
        console.log(post);
        this.LoadPost();
      });
    });
  }

  LoadPost() {
    this.postService.getPagedData(this.Paging).subscribe(
      res => {
        this.Posts = res.items;
        console.log(res);
      },
      err => {
        console.log(err);
      }
    );
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
    this.CreatePost.userId = this.UserService.getUser().id; // Set current userId (replace with actual userId)
    this.postService.createPost(this.CreatePost).subscribe(
      response => {
        console.log('Post created successfully', response);
        this.LoadPost(); // Refresh posts
        // Reset form
        this.messageContent = '';
        this.CreatePost.files = [];
        this.imageSrcs = [];
      },
      error => {
        console.error('Error creating post', error);
      }
    );
  }
}
