export interface CreateNotification {
    content: string;
    postId?: number;
    isSeen: boolean;
    senderId: string;
    receiverId: string;
  }
  