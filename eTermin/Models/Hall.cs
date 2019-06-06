using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Hall
    {
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public Sport Sport { get; set; }
        public int SportCentreId { get; set; }

        public virtual SportCentre SportCentre { get; set; }
    }
}