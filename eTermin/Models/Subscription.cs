using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Subscription
    {
        [ScaffoldColumn(false)]
        public int SubscriptionID { get; set; }
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Note { get; set; }
        [Required]
        public int HallID { get; set; }
        [Required]
        public int UserID { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual User User { get; set; }
    }
}