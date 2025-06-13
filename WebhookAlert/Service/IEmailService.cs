using DedicatedService.DTOs;

namespace WebhookAlert.Service
{
    public interface IEmailService
    {
        string SendEmail(EmailDTO emailDTO);
    }
}
