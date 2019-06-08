using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Maps;
using Google.Maps.StaticMaps;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            //always need to use YOUR_API_KEY for requests.  Do this in App_Start.
            GoogleSigned.AssignAllServices(new GoogleSigned("AIzaSyAcNPtqdgXh8QbwLpPwLAGDCoCsikfKlj4"));
            var map = new StaticMapRequest();
            map.Center = new Location("1600 Pennsylvania Ave NW, Washington, DC 20500");
            //map.Size = new System.Drawing.Size(400, 400);
            map.Size = new MapSize(400, 400);
            map.Zoom = 14;
            ViewBag.StaticMapUri = map.ToUri();
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