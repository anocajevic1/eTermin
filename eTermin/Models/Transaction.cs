using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTermin.Models
{
    public class Transaction
    {
        [ScaffoldColumn(false)]
        public int TransactionID { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Amount { get; set; }
        public string Note { get; set; }
        [Required]
        public int SportCentreID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int EmployeeID { get; set; }

        public virtual SportCentre SportCentre { get; set; }
        public virtual User User { get; set; }
        public virtual Employee Employee { get; set; }
    }
}