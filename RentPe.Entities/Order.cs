using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class Order:BaseEntity
    {
        public string OrderNo { get; set; }
        public string Owner { get; set; }
        public float AmountPaid { get; set; }
        public float AmountRemain { get; set; }
        public float TotalAmount { get; set; }
        public string Renter { get; set; }
        public string Item { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string VideoOfUnboxing { get; set; }
        public string VideoOfPacking { get; set; }
    }
}
