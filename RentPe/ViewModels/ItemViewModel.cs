using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class ItemListingViewModel
    {
        public List<RentItem> RentItems { get; set; }
        public string SearchTerm { get; set; }
    }
    public class ItemActionViewModel
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string UserID { get; set; }
        public bool Favourites { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        public DateTime RentingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ItemSize { get; set; }
        public string ItemColor { get; set; }
        public bool Negotiable { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Brand { get; set; }
        public string Condition { get; set; }
        public DateTime EntryDate { get; set; }
        public string Location { get; set; }
        public int RentingPeriod { get; set; }
        public string Note { get; set; }
        public string Label { get; set; }
    } 
        
}