using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class ApplicationUser:IdentityUser
    {
        
        public string Name { get; set; }
        public string ?Address { get; set; }
        public string ?Image { get; set; }
        public string ?ImageBackground { get; set; }
        public Gender Gender { get; set; }
        public DateTime ?Birthday { get; set; }

        public virtual ICollection<InforUser> InforUsers { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        //public virtual ICollection<Message> Messages { get; set; }
        //public virtual ICollection<GroupMessage> GroupMessages { get; set; }
        //public virtual ICollection<Message> ReceivedMessages { get; set; }
        //public virtual ICollection<Friendship> Friendships { get; set; }
        //public virtual ICollection<Friendship> Friendships { get; set; }
        //public virtual ICollection<GroupMember> GroupMemberships { get; set; }
        //public virtual ICollection<Conversation> ConversationsUser1 { get; set; }
        //public virtual ICollection<Conversation> ConversationsUser2 { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
