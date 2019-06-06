using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public partial class Employee : Person {
        public int SportCentreID { get; set; }

        public virtual SportCentre SportCentre { get; set; }
    }
}
