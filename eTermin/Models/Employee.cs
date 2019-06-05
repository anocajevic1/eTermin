using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Employee : Person {
        public int EmployeeId { get; set; }
        public int SportCentreId { get; set; }

        public virtual SportCentre SportCentre { get; set; }
    }
}
