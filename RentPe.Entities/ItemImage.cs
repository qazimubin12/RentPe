using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class ItemImage:BaseEntity
    {
        public int ItemID { get; set; }
        public string ImageURL { get; set; }
    }
}
