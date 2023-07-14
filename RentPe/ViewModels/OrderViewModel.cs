using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class OrderListingViewModel
    {
        public List<OrderViewModelNew> Orders { get; set; }
        public string SearchTerm { get; set; }
    }

    public class OrderViewModelNew
    {
        public User Owner { get; set; }
        public User Renter { get; set; }
        public Ad Ad { get; set; }
        public Order Order { get; set; }
    }

    public class OrderActionViewModel
    {
        public List<User> Owners { get; set; }
        public List<User> Renters { get; set; }
        public List<Ad> Ads { get; set; }
        public string OrderNo { get; set; }
        public User OwnerFull { get; set; }
        public string Owner { get; set; }
        public float AmountPaid { get; set; }
        public float AmountRemain { get; set; }
        public float TotalAmount { get; set; }
        public string Renter { get; set; }
        public User RenterFull { get; set; }
        public string Item { get; set; }
        public User AdFull { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string VideoOfUnboxing { get; set; }
        public string VideoOfPacking { get; set; }
        public List<string> StatusList { get; internal set; }
        public int ID { get; internal set; }
    }
}