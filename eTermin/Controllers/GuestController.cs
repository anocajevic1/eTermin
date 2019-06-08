using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View("GuestHomeForm");
        }
        public IActionResult TabSportsCentres()
        {
            return View("GuestSportsCentres");
        }
        public IActionResult TabAbout()
        {
            return View("GuestAbout");
        }
        public IActionResult GuestSignUp_OnClick()
        {
            return View("../RegisterUser/RegistrationForm");
        }
        public IActionResult GuestSignIn_OnClick()
        {
            return View("../Login/Index");
        }
    }
}