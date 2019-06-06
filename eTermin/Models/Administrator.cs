using System;
namespace eTermin.Models
{
    public class Administrator
    {
            public int AdministratorId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int AccessLevel { get; set; }  
    }
}
