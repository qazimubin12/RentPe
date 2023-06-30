using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class AdListingViewModel
    {
        public List<Ad> Ads { get; set; }
        public string SearchTerm { get; set; }
    }

    //public class AdWithTime
    //{
    //    public Ad Ad { get; set; }
    //    public string TimeInformation { get; set; }
    //}

    public class AdActionViewModel
    {
        public List<UserRating> UserRatings { get; set; }
        public List<AdWithTimeModel> RelatedAds { get; set; }
        public List<ItemCategory> ItemCategories { get; set; }
        public List<string> otherImages { get; set; }
        public int ID { get; set; }
        public string MainImage { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public bool Privacy { get; set; }
        public string ItemName { get; set; }
        public string Tag { get; set; }

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