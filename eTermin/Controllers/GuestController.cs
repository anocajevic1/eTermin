using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers {
    public class GuestController : Controller {
        public static DateTime selectedDate;
        public static string selectedSportCentre;
        public static string selectedSport;
        public IActionResult Index() {
            return View("GuestHome");
        }
        public IActionResult TabSportsCentres() {
            return View("GuestSportsCentres");
        }
        public IActionResult TabAbout() {
            return View("GuestAbout");
        }
        public IActionResult GuestSignUp_OnClick() {
            return View("../RegisterUser/RegistrationForm");
        }
        public IActionResult GuestSignIn_OnClick() {
            return View("../Login/Index");
        }
        public IActionResult Search(DateTime etDate, string etSportCentre, string etSport)
        {
            selectedDate = etDate;
            selectedSportCentre = etSportCentre;
            selectedSport = etSport;
            return View("GuestSportsCentres");
        }
    }
}