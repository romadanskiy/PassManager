using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace PasswordManager
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта PasswordManager", "passmanager@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Headers.Add("Precedence", "bulk");
            emailMessage.Subject = subject;
            emailMessage.Date = DateTimeOffset.Now;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                //await client.ConnectAsync("smtp.yandex.ru", 465, true);
                //await client.AuthenticateAsync("passmanager@yandex.ru", "nlffkrvbycmlzwee");
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("passmanager.info@gmail.com", "PAROLitis123");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
