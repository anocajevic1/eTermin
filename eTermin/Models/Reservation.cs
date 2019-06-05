using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Reservation {
        public int ReservationId { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTime { get; set; }
        public int HallId { get; set; }
        public int PersonId { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual Person Person { get; set; }
    }
}
