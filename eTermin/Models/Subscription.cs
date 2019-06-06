using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Time { get; set; }
        public string Note { get; set; }
        public int HallId { get; set; }
        public int UserId { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual User User { get; set; }
    }
}