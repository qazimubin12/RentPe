using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Services
{
    public class CategoryServices
    {
        #region Singleton
        public static CategoryServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoryServices();
                return instance;
            }
        }
        private static CategoryServices instance { get; set; }
        private CategoryServices()
        {
        }
        #endregion





        public List<ItemCategory> GetRentItemCategories(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.ItemCategories.Where(p => p.CategoryName != null && p.CategoryName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.CategoryName)
                                            .ToList();
                }
                else
                {
                    return context.ItemCategories.OrderBy(x => x.CategoryName).ToList();
                }
            }
        }





        public ItemCategory GetItemCategory(int ID)
        {
            using (var context = new DSContext())
            {

                return context.ItemCategories.Find(ID);

            }
        }

        public void SaveItemCategory(ItemCategory ItemCategory)
        {
            using (var context = new DSContext())
            {
                context.ItemCategories.Add(ItemCategory);
                context.SaveChanges();
            }
        }

        public void UpdateItemCategory(ItemCategory ItemCategory)
        {
            using (var context = new DSContext())
            {
                context.Entry(ItemCategory).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteItemCategory(int ID)
        {
            using (var context = new DSContext())
            {

                var ItemCategory = context.ItemCategories.Find(ID);
                context.ItemCategories.Remove(ItemCategory);
                context.SaveChanges();
            }
        }
    }
}

