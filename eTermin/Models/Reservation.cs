using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Reservation {
        [ScaffoldColumn(false)]
        public int ReservationID { get; set; }
        [Required]
        public DateTime DateTimeCreated { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int HallID { get; set; }
        [Required]
        public int PersonID { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual Person Person { get; set; }
    }
}
