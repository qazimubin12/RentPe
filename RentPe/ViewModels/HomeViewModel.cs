using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class HomeViewModel
    {
        public List<AdWithTimeModel> ExclusiveAds { get; set; }
        public List<AdWithTimeModel> FeaturedAds { get; set; }
        public List<AdWithTimeModel> LatestAds { get; set; }
    }
    public class HomeShopViewModel
    {
        public List<ItemCategory> ItemsCategories { get; set; }
        public List<AdWithTimeModel> Ads { get; set; }
    }


    public class AdWithTimeModel
    {
        public Ad Ad { get; set; }
        public string Time { get; set; }
    }
    public class ProductViewModel
    {
        public List<ItemCategory> ItemCategories { get; set; }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public bool Privacy { get; set; }
        public string ItemName { get; set; }
        public string UserID { get; set; }
        public string ItemDescription { get; set; } // Show Contact Number //Hide Contact Number
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Authenticity { get; set; }

        public string ItemCategory { get; set; }
        public string Type { get; set; } //Men or Women
        public bool Negotiable { get; set; }
        public string Condition { get; set; }
        public DateTime EntryDate { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }


        public string AdStatus { get; set; }

        public int RentingPeriod { get; set; }
        public string Featured { get; set; }
    }
}