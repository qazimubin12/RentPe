using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Entities
{
    public class Conversation:BaseEntity
    {
        public int Item { get; set; }
        public string Message { get; set; }
        public string SentBy { get; set; }
        public string SentByName { get; set; }
        public string RecievedBy { get; set; }
        public string Attachments { get; set; }
        public DateTime Date { get; set; }
    }
}
