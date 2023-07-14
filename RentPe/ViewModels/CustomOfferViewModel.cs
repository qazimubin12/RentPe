using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentPe.Entities;

namespace RentPe.ViewModels
{
    public class CustomOfferListingViewModel
    {
        public List<CustomOfferModel> CustomOffers { get; set; }
        public string SearchTerm { get; set; }
    }

    public class CustomOfferModel
    {
        public CustomOffer CustomOffer { get; set; }
        public Ad Ad { get; set; }
        public User Renter { get; set; }
        public User Owner { get; set; }
    }
    public class CustomOfferActionViewModel
    {
        public List<string> StatusList { get; set; }
        public List<User> Owners { get; set; }
        public List<User> Rentees { get; set; }
        public List<Ad> Ads { get; set; }
        public int ID { get; set; }
        public string Owner { get; set; }
        public User OwnerFull { get; set; }
        public DateTime OfferDate { get; set; }
        public string Rentee { get; set; }
        public User RenterFull { get; set; }
        public int Item { get; set; }
        public Ad AdFull { get; set; }
        public string Status { get; set; }  //ACCEPTED //DECLINE // PENDING //WITHDRAWN
        public float OfferedPrice { get; set; }
        public DateTime RentingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentingPeriod { get; set; }
    }
}