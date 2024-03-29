﻿using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class UserDashboardViewModel
    {
        public string ChatActiveOrNot { get; set; }
        public User SignedInUser { get; set; }
        public User Owner { get; set; }
        public Ad Ad { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<CustomerOfferModel> CustomOffers { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public string Name { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string SearchTerm { get; set; }
        public List<Conversation> Chats { get; set; }
        public List<MainChatModel> MainChats { get; set; }
        public List<ChatInboxModel> InboxList { get;  set; }
        public User Rentee { get;  set; }
    }


    public class MainChatModel
    {
        public Conversation Chats { get; set; }
        public CustomOffer CustomOffer { get; set; }
    }
    
    public class ChatInboxModel
    {
        public User SentBy { get; set; }
        public User RecievedBy { get; set; }
        public string Message { get; set; }
        public int Item { get; set; }
        public DateTime Date { get;  set; }
    }

    public class OwnerDashboardViewModel
    {
        public User SignedInUser { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<CustomerOfferModel> CustomOffers { get; set; }
        public string Name { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string SearchTerm { get; set; }

    }

    public class PaymentViewModel
    {
        public float AmountPaid { get; set; }
        public User Renter { get; set; }
        public Ad AdFull { get; set; }
        public string Name { get; set; }
        public Order OrderFull { get; set; }
        public string  Remarks { get; set; }
        public string Proof { get; set; }
        public int OrderID { get; set; }
    }


    public class OrderViewModel
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public string Owner { get; set; }
        public User OwnerFull { get; set; }
        public float AmountPaid { get; set; }
        public float AmountRemain { get; set; }
        public float TotalAmount { get; set; }
        public string Renter { get; set; }
        public User RenterFull { get; set; }
        public string Item { get; set; }
        public Ad ItemFull { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Packing { get; set; }
        public string VideoOfUnboxing { get; set; }
        public string VideoOfPacking { get; set; }
    }

    public class CustomerOfferModel
    {
        public int ID { get; set; }
        public User Owner { get; set; }
        public DateTime OfferDate { get; set; }
        public User Rentee { get; set; }
        public Ad Item { get; set; }
        public float OfferedPrice { get; set; }
        public string Status { get; set; }  //ACCEPTED //DECLINE // PENDING
        public DateTime RentingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentingPreiod { get; set; }
    }
}