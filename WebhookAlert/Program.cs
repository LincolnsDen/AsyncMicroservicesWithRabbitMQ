using DedicatedService.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebhookAlert.Consumer;
using WebhookAlert.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IEmailService, EmailServiceRepo>();
builder.Services.AddMassTransit(
    x =>
    {
        x.AddConsumer<EmailWebhookConsumer>();
        x.UsingRabbitMq((context, config) =>
        {
            config.Host("rabbitmq://localhost", cfg =>
            {
                cfg.Username("guest");
                cfg.Password("guest");
            });
            config.ReceiveEndpoint("emai-webhook-queue", endP =>
            {
                endP.ConfigureConsumer<EmailWebhookConsumer>(context);
            });
        });
    });

var app = builder.Build();

app.MapPost("/email-webhook", ([FromBody] EmailDTO emailDTO,
    IEmailService emailService) =>
    {
        string result = emailService.SendEmail(emailDTO);
        return Task.FromResult(result);
    });

app.Run();
