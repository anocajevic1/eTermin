using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Log {
        [ScaffoldColumn(false)]
        public int LogID { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        [Required]
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
