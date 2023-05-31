using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class AdImage:BaseEntity
    {
        public string AdID { get; set; }
        public string ImageURL { get; set; }
    }
}
