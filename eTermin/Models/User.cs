using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public partial class User : Person {
        [Required]
        public double Balance { get; set; }
        public string Photo { get; set; }
    }
}
