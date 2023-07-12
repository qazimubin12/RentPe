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
        
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<AdImage> AdImages { get; set; }
        public DbSet<CustomOffer> CustomOffers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Query> Queries { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
