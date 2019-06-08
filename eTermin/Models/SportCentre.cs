using System;
using System.ComponentModel.DataAnnotations;

namespace eTermin.Models {
    public class SportCentre {
        [ScaffoldColumn(false)]
        public int SportCentreID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
