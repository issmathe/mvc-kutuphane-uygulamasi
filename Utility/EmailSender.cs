using Microsoft.AspNetCore.Identity.UI.Services;

namespace UdemyProje.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //email gönderme işlemleri burada 
            return Task.CompletedTask;
        }
    }
}
