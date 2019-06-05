using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class User : Person {
        public int UserId { get; set; }
        public double Balance { get; set; }
        public string Photo { get; set; }
    }
}
