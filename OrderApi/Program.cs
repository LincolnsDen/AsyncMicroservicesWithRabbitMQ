using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderApi.Consumer;
using OrderApi.Data;
using OrderApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrder,OrderRepo>();
builder.Services.AddDbContext<OrderDbContext>(o=>o.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ItemConsumer>();
    x.UsingRabbitMq((contex, config) =>
    {
        config.Host("rabbitmq://localhost", cfg =>
        {
            cfg.Username("guest");
            cfg.Password("guest");
        });
        config.ReceiveEndpoint("items-queue", endP =>
        {
            endP.ConfigureConsumer<ItemConsumer>(contex);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
