using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class UserRating:BaseEntity
    {
        public string UserID { get; set; }
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
