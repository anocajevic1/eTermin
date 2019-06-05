using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public String dayOfWeek { get; set; }
        public DateTime time { get; set; }
        public String note { get; set; }
        public int HallId { get; set; }
        public int UserId { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual User User { get; set; }
    }
}