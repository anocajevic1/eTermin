using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Hall {
        [ScaffoldColumn(false)]
        public int HallID { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Sport Sport { get; set; }
        [Required]
        public int SportCentreID { get; set; }

        public virtual SportCentre SportCentre { get; set; }
    }
}