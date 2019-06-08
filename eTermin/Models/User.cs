using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class User : Person {
        //public int UserID { get; set; }
        [Required]
        public double Balance { get; set; }
        public string Photo { get; set; }
    }

}
