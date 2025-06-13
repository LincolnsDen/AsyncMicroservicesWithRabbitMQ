using DedicatedService.DTOs;
using MassTransit;

namespace WebhookAlert.Consumer
{
    public class EmailWebhookConsumer(HttpClient client) : IConsumer<EmailDTO>
    {
        public async Task Consume(ConsumeContext<EmailDTO> context)
        {
            var result = await client.PostAsJsonAsync("https://localhost:7162/email-webhook",
                new EmailDTO(context.Message.Title, context.Message.EmailContent));
            result.EnsureSuccessStatusCode();
        }
    }
}
