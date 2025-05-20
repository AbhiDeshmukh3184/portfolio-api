using MyInfoCommonUtility.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyInfoDAL.EmailRepo
{
    public class EmailRepository : IEmailRepository
    {
        private IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string emailbody, string Subject)
        {
            ResultWithDataDTO<int> resultWithDataBO = new ResultWithDataDTO<int>
            {
                IsSuccessful = false
            };
            try
            {
                string environment = _configuration["MyInfoData:Environment"]??"";
                if (environment != "DEV")
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add("abhideshmukh3184@gmail.com");
                    mail.From = new MailAddress("abhideshmukh3184@gmail.com", "Exception Alert");
                    string url = @$"{environment}";
                    mail.Subject = $"{Subject} from MyInfo Project | {url}";
                    mail.Body = emailbody;
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("abhideshmukh3184@gmail.com", "Abhi@3184");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
