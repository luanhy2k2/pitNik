using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FriendShip
{
    public class MyFriendDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public int TotalPost {  get; set; }
        public int TotalImage { get; set; }
        public int TotalFriend { get; set; }
        public int MutualFriend { get; set; }   
        public DateTime Created { get; set; }
    }
}
