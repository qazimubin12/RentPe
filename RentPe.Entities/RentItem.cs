using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class RentItem:BaseEntity
    {
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

    }
}
