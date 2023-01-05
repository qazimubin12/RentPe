using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    
    public  class UserInfo:BaseEntity
    {
        public string Address { get; set; }
        public string Photo { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public DateTime MemberSince { get; set; }

    }
}
