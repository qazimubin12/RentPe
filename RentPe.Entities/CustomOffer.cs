using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class CustomOffer:BaseEntity
    {
        public int Owner { get; set; }
        public DateTime OfferDate { get; set; }
        public int Rentee { get; set; }
        public int Item { get; set; }
        public float OfferedPrice { get; set; }
        public string Status { get; set; }
        public DateTime RentingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentingPreiod { get; set; }
    }
}
