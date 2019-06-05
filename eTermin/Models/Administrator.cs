using System;
namespace eTermin.Models
{
    public class Administrator
    {
            public int AdministratorId { get; set; }
            public String username { get; set; }
            public String password { get; set; }
            public int accessLevel { get; set; }  
    }
}
