using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class HomeShopViewModel
    {
        public List<ItemCategory> ItemsCategories { get; set; }
        public List<RentItem> Items { get; set; }
    }
}