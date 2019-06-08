using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;

namespace eTermin.Controllers
{
    public class RegisterUserController : Controller
    {
        public IActionResult Index()
        {
           
            return View("RegistrationForm");
        }
        [HttpPost]
        public IActionResult Validate(string etFirstName,string etLastName,string etEmail,string etUsername,string etPassword,string etConfirmPassword)
        {
            Boolean validationOk = true;
            if (etFirstName.Equals("") || etLastName.Equals("") || etUsername.Equals("") || etPassword.Equals("") || etConfirmPassword.Equals("") || etEmail.Equals("")) validationOk = false;
            if (!etPassword.Equals(etConfirmPassword)) validationOk = false;
            var baza = DatabaseContext.getInstance();
            List<User> users = baza.Person.OfType<User>().ToList();
            for (int i = 0; i < users.Count; i++)
                if (users[i].Username.Equals(etUsername))
                    validationOk = false;
            if( validationOk)
            {
                baza.Person.Add(new User
                {
                    FirstName = etFirstName,
                    LastName = etLastName,
                    Username = etUsername,
                    Password = etPassword,
                    Email = etEmail,
                    Balance = 0,
                    Photo = null
                });
                baza.SaveChanges();
                return View("../Login/Index");
            }
            return View("RegistrationForm");
        }
    }
}