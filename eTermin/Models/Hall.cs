using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Hall
    {
        public int HallId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int duration { get; set; }
        public int price { get; set; }
        public String sport { get; set; }
        public int SportCentreId
        { get; set;    }

        public virtual SportCentre SportCentre { get; set; }
    }
}