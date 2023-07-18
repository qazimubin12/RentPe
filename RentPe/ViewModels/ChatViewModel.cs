using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class ChatListingViewModel
    {
        public List<User> Users { get; set; }
        public List<MainChatModel> Chats { get; set; }
        public string SentBy { get; set; }
        public string RecievedBy { get; set; }
        public User USentBy { get; set; }
        public User URecievedBy { get; set; }
    }

    public class ChatActionViewModel
    {
        public int ID { get; set; }
    }
}