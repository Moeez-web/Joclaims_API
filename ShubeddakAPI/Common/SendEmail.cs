using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace ShubeddakAPI.Common
{
    public static class SendEmail
    {
        public static void sendEmail(string recipientEmail, string body, string subject)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Bcc.Add(recipientEmail);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                System.Net.NetworkCredential networkcred = new System.Net.NetworkCredential();
                networkcred.UserName = ConfigurationManager.AppSettings["Email"].ToString();
                networkcred.Password = ConfigurationManager.AppSettings["Password"].ToString();
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = networkcred;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
        }



        public static int sendEmailwithCC(string senderEmail, string subject, string message, string customerEmail)
        {
            try
            {

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(senderEmail);
                if (customerEmail != null && customerEmail != "")
                {
                    mailMessage.CC.Add(customerEmail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.office365.com";
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
                System.Net.NetworkCredential networkcred = new System.Net.NetworkCredential();
                networkcred.UserName = ConfigurationManager.AppSettings["Email"].ToString();
                networkcred.Password = ConfigurationManager.AppSettings["Password"].ToString();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkcred;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}