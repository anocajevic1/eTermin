using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models {
    public class Log {
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
