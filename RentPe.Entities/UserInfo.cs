using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    
    public  class UserInfo:BaseEntity
    {
        public string UserID { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public string NIC { get; set; }
        public DateTime MemberSince { get; set; }

        public string Rating { get; set; }
    }
}
