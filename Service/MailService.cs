using Microsoft.AspNetCore.Hosting.Server;
using System.Net;
using System.Net.Mail;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class MailService
    {
        private MailService()
        {
            AdminAddress = new MailAddress(SysConfigModel
                                          .Configuration
                                          .GetConnectionString("MailUserName"),
                                          "Admin");
        }
        MailAddress AdminAddress { get; set; }
        private static MailService Instance = new MailService();
        public static MailService GetInstance()
        {
            Instance ??= new MailService();
            return Instance;
        }

        public void SendMail(string? recipiant, string[]? ccList,
                             string subject, string body)
        {
            if (recipiant == null)
            {
                throw new ArgumentNullException(nameof(recipiant),
                                                "recipiant is null");
            }
            var toAddress = new MailAddress(recipiant);
            var message = new MailMessage(AdminAddress, toAddress)
                          {
                              Subject = subject,
                              Body = body
                          };

            if (ccList != null)
            {
                foreach (string cc in ccList)
                {
                    message.CC.Add(new MailAddress(cc));
                }
            }

            var userName = SysConfigModel
                          .Configuration
                          .GetConnectionString("MailUserName");
            var password = SysConfigModel
                          .Configuration
                          .GetConnectionString("MailPassword");
            var client = new SmtpClient(SysConfigModel
                                       .Configuration
                                       .GetConnectionString("MailHost"))
                         {
                             UseDefaultCredentials = false,
                             Credentials = new System.Net
                                              .NetworkCredential(userName,password),
                             EnableSsl = true,
                         };

            client.Send(message);
        }

    }
}
