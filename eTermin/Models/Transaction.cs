using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime time { get; set; }
        public int amount { get; set; }
        public String note { get; set; }
        public int SportCentreId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }

        public virtual SportCentre SportCentre { get; set; }
        public virtual User User { get; set; }
        public virtual Employee Employee { get; set; }
    }
}