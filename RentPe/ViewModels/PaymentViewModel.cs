using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class PaymentListingViewModel
    {
        public List<PaymentModel> Payments { get; set; }
        public string SearchTerm { get; set; }
    }

    public class PaymentModel
    {
        public Payment PaymentFull { get; set; }
        public Order OrderFull { get; set; }
        public User RenterFul { get; set; }
        public User OwnerFull { get; set; }

    }
    public class PaymentActionViewModel
    {
        public int ID { get; set; }
        public Order OrderFull { get; set; }
        public string Proof { get; set; }
        public string Remarks { get; set; }
        public string Name { get; set; }
        public int OrderID { get; set; }
    }
}