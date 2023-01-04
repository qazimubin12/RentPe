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





    }
}
