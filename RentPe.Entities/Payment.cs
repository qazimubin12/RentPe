using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class Payment:BaseEntity
    {
        public string Proof { get; set; }
        public string Remarks { get; set; }
        public string Name { get; set; }
        public int OrderID { get; set; }
    }
}
