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
    public class UserRatingServices
    {
        #region Singleton
        public static UserRatingServices Instance
        {
            get
            {
                if (instance == null) instance = new UserRatingServices();
                return instance;
            }
        }
        private static UserRatingServices instance { get; set; }
        private UserRatingServices()
        {
        }
        #endregion





        public List<UserRating> GetUserRatings(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.UserRatings.Where(p => p.Name != null && p.Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Name)
                                            .ToList();
                }
                else
                {
                    return context.UserRatings.OrderBy(x => x.Name).ToList();
                }
            }
        }



        public List<UserRating> GetUserRating(string ID)
        {
            using (var context = new DSContext())
            {

                return context.UserRatings.Where(x=>x.UserID == ID).ToList();   

            }
        }


        public UserRating GetUserRating(int ID)
        {
            using (var context = new DSContext())
            {

                return context.UserRatings.Find(ID);

            }
        }

        public void SaveUserRating(UserRating UserRating)
        {
            using (var context = new DSContext())
            {
                context.UserRatings.Add(UserRating);
                context.SaveChanges();
            }
        }

        public void UpdateUserRating(UserRating UserRating)
        {
            using (var context = new DSContext())
            {
                context.Entry(UserRating).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUserRating(int ID)
        {
            using (var context = new DSContext())
            {

                var UserRating = context.UserRatings.Find(ID);
                context.UserRatings.Remove(UserRating);
                context.SaveChanges();
            }
        }
    }
}

