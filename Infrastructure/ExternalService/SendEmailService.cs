using Core.Interface.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalService
{
    public class SendEmailService : ISendEmailService
    {
        public async Task<bool> SendEmail(string email, string body)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.To.Add(email);
                    message.Subject = "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi";
                    message.From = new MailAddress("luan2k2hy@gmail.com");
                    message.Body = body;
                    using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential("luan2k2hy@gmail.com", "sgyg nyhs xdvk uuma");

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
