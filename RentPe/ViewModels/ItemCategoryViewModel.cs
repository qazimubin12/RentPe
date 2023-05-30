using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe.ViewModels
{
    public class ItemCategoryListingViewModel
    {
        public List<ItemCategory> ItemCategories { get; set; }
        public string SearchTerm { get; set; }
    }

    public class ItemCategoryActionViewModel
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

    }
}