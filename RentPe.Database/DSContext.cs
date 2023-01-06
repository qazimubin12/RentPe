using Microsoft.AspNet.Identity.EntityFramework;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Database
{
    public class DSContext :IdentityDbContext<User>,IDisposable
    {
        public DSContext() : base("RPConnectionStrings")
        {

        }

        public static DSContext Create()
        {
            return new DSContext();
        }
        //Table NAme hoga

        //Singular       Plural
        //public DbSet<EntityKaNam> TableKaNaam { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<RentItem> RentItems { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<CustomOffer> CustomOffers { get; set; }


    }
}
