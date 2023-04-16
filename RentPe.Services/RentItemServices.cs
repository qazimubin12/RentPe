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
    public class RentItemServices
    {
        #region Singleton
        public static RentItemServices Instance
        {
            get
            {
                if (instance == null) instance = new RentItemServices();
                return instance;
            }
        }
        private static RentItemServices instance { get; set; }
        private RentItemServices()
        {
        }
        #endregion

        public List<RentItem> GetRentItem(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.RentItems.Where(p => p.ItemName != null && p.ItemName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.ItemName)
                                            .ToList();
                }
                else
                {
                    return context.RentItems.OrderBy(x => x.ItemName).ToList();
                }
            }
        }

       



        public RentItem GetRentItem(int ID)
        {
            using (var context = new DSContext())
            {

                return context.RentItems.Find(ID);

            }
        }

        public void SaveRentItem(RentItem RentItem)
        {
            using (var context = new DSContext())
            {
                context.RentItems.Add(RentItem);
                context.SaveChanges();
            }
        }

        public void UpdateRentItem(RentItem RentItem)
        {
            using (var context = new DSContext())
            {
                context.Entry(RentItem).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteRentItem(int ID)
        {
            using (var context = new DSContext())
            {

                var RentItem = context.RentItems.Find(ID);
                context.RentItems.Remove(RentItem);
                context.SaveChanges();
            }
        }























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

