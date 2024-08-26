using Core.Interface.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ExternalService
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string email, string body)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var fromEmail = emailSettings["Email"];
                var password = emailSettings["Password"];
                var host = emailSettings["Host"];
                var port = int.Parse(emailSettings["Port"]);

                using (var message = new MailMessage())
                {
                    message.To.Add(email);
                    message.Subject = "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi";
                    message.From = new MailAddress(fromEmail);
                    message.Body = body;

                    using (var smtp = new SmtpClient(host, port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(fromEmail, password);

                        await smtp.SendMailAsync(message);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
