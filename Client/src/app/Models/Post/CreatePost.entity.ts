export interface CreatePost {
    userId: string;
    content: string;
    files: File[];
    id:number;
    created:Date
    groupId:number
  } 