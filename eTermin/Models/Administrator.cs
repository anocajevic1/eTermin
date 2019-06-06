using System;
using System.ComponentModel.DataAnnotations;

namespace eTermin.Models
{
    public class Administrator
    {
        [ScaffoldColumn(false)]
            public int AdministratorID { get; set; }
            [Required]
            public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int AccessLevel { get; set; }  
    }
}
