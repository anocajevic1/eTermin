using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace eTermin.Models {
    public class Email {
        public static void Send(string to, string subject, string body) {
            var fromAddress = new MailAddress("eindexfp@gmail.com", "eTermin");
            var toAddress = new MailAddress(to);
            const string fromPassword = "etf18120FP";

            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) {
                Subject = subject,
                Body = body
            }) {
                message.IsBodyHtml = true;
                smtp.Send(message);
            }
        }
    }
}
