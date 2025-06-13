using DedicatedService.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WebhookAlert.Service
{
    public class EmailServiceRepo : IEmailService
    {
        public string SendEmail(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("eryn89@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("eryn89@ethereal.email"));
            email.Subject = emailDTO.Title;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text= emailDTO.EmailContent
            };
            var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587 , SecureSocketOptions.StartTls);
            smtp.Authenticate("eryn89@ethereal.email", "JZ64BP5qz3DnSnJ5x3", CancellationToken.None);
            smtp.Send(email);
            smtp.Disconnect(true);
            return "Email Sent Succsessfully";
        }
    }
}
