using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RentPe.Services
{
    public class CustomOfferServices
    {
        #region Singleton
        public static CustomOfferServices Instance
        {
            get
            {
                if (instance == null) instance = new CustomOfferServices();
                return instance;
            }
        }
        private static CustomOfferServices instance { get; set; }
        private CustomOfferServices()
        {
        }
        #endregion

        public List<CustomOffer> GetCustomOffers()
        {
            using (var context = new DSContext())
            {

                return context.CustomOffers.ToList();
                
            }
        }




        public List<CustomOffer> GetCustomOfferByRentee(string UserID)
        {
            using (var context = new DSContext())
            {

                return context.CustomOffers.Where(x=>x.Rentee ==  UserID).ToList();    

            }
        }


        public CustomOffer GetCustomOffer(int ID)
        {
            using (var context = new DSContext())
            {

                return context.CustomOffers.Find(ID);

            }
        }

        public void SaveCustomOffer(CustomOffer CustomOffer)
        {
            using (var context = new DSContext())
            {
                context.CustomOffers.Add(CustomOffer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomOffer(CustomOffer CustomOffer)
        {
            using (var context = new DSContext())
            {
                context.Entry(CustomOffer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCustomOffer(int ID)
        {
            using (var context = new DSContext())
            {

                var CustomOffer = context.CustomOffers.Find(ID);
                context.CustomOffers.Remove(CustomOffer);
                context.SaveChanges();
            }
        }
    }
}
