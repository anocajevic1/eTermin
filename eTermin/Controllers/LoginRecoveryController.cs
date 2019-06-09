using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers {
    public class LoginRecoveryController : Controller {
        private DatabaseContext database = DatabaseContext.getInstance();
        private static string code;
        private static string username;

        public IActionResult StartPasswordRecovery() {
            return View("RecoveryForm1");
        }
        private string getCode(int length) {
            Random rnd = new Random();
            return rnd.Next((int)Math.Pow(10, length - 1), (int) Math.Pow(10, length)).ToString();
        }

        private string EmailRecoveryBody(string username, string code) {
            return "Poštovanje,<br/><br/>" +
                    "Primili smo zahtjev za pristup vašem računu " + username +
                    " putem aplikacije eTermin. Ispod se nalazi vaš kontrolni kod:<br/><br/>" +
                    "<div style=\"text-align:center\"><strong style=\"font-size:24px; font-weight:bold\">" + code +
                    "</strong></div>" +
                    "<br/><br/>Ako niste zatražili ovaj kod, možda netko drugi pokušava pristupiti vašem računu. Ne prosljeđujte ovaj kod i ne otkrivajte ga nikome.<br/><br/>" +
                    "Lijep pozdrav,<br/>" +
                    "osoblje eTermin";
        }

        public IActionResult SendRecoveryCode(string etEmail) {
            username = database.GetUsername(etEmail);
            code = getCode(6);
            if (username == null)
                return View("../Login/Index");
            Email.Send(etEmail, "Zahtjev za resetovanje šifre", EmailRecoveryBody(username, code));
            return View("RecoveryForm2", code);
        }

        public IActionResult Recover(string etRecoveryCode) {
            if (!etRecoveryCode.Equals(code))
                return View("../Login/Index");
            return View("RecoveryForm3");
        }

        public IActionResult ResetPassword(string etPassword, string etPasswordConfirm) {
            if (!etPassword.Equals(etPasswordConfirm))
                return View("RecoveryForm3");
            database.UpdatePassword(username, etPassword);
            return View("../Login/Index");
        }
    }
}