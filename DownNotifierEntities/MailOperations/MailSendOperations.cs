using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.MailOperations
{
    public static class MailSendOperations
    {
        /// <summary>
        /// e-mail delivery notification processes
        /// </summary>
        /// <param name="email"></param>
        /// <param name="targetAppUrl"></param>
        /// <returns></returns>
        public static bool SendMail(string email, string? targetAppUrl)
        {
            try
            {
                var fromAddress = new MailAddress("minebyrkdr@gmail.com", "Mine Güldaş");
                var toAddress = new MailAddress(email, "To Name");
                string fromPassword = "fltvneoztjkycdlc";
                string subject = "Mine Güldaş - Down Notifier Mail Notification";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    EnableSsl = true,
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = "Hello, Your Target Application called by " + targetAppUrl + " was down.",
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
