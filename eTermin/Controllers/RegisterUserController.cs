using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers
{
    public class RegisterUserController : Controller
    {
        public IActionResult RegistrationForm()
        {
            return View();
        }
    }
}