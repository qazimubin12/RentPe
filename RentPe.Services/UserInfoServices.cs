using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RentPe.Services
{
    public class UserInfoServices
    {
        #region Singleton
        public static UserInfoServices Instance
        {
            get
            {
                if (instance == null) instance = new UserInfoServices();
                return instance;
            }
        }
        private static UserInfoServices instance { get; set; }
        private UserInfoServices()
        {
        }
        #endregion

        public List<UserInfo> GetUserInfos(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.UserInfos.Where(p => p.NIC != null && p.NIC.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.NIC)
                                            .ToList();
                }
                else
                {
                    return context.UserInfos.OrderBy(x => x.NIC).ToList();
                }
            }
        }




       
        public UserInfo GetUserInfo(string UserID)
        {
            using (var context = new DSContext())
            {

                return context.UserInfos.Where(x=>x.UserID ==  UserID).FirstOrDefault();    

            }
        }


        public UserInfo GetUserInfo(int ID)
        {
            using (var context = new DSContext())
            {

                return context.UserInfos.Find(ID);

            }
        }

        public void SaveUserInfo(UserInfo UserInfo)
        {
            using (var context = new DSContext())
            {
                context.UserInfos.Add(UserInfo);
                context.SaveChanges();
            }
        }

        public void UpdateUserInfo(UserInfo UserInfo)
        {
            using (var context = new DSContext())
            {
                context.Entry(UserInfo).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUserInfo(int ID)
        {
            using (var context = new DSContext())
            {

                var UserInfo = context.UserInfos.Find(ID);
                context.UserInfos.Remove(UserInfo);
                context.SaveChanges();
            }
        }
    }
}
