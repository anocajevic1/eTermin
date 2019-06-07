using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace eTermin.Models {
    public class Employee : Person {
        //public int EmployeeID { get; set; }
        public int SportCentreID { get; set; }

        public virtual SportCentre SportCentre { get; set; }
    }
}
