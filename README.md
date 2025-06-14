# 🚀 Asynchronous Microservices with RabbitMQ & MassTransit  

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)  [![.NET Core](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)  [![Docker](https://img.shields.io/badge/Docker-✓-2496ED.svg)](https://www.docker.com/)  

**A scalable, decoupled microservice architecture** using **RabbitMQ** and **MassTransit** for asynchronous communication, with **email webhooks** for real-time notifications.  

---

### 🌟 Key Features  
- **Asynchronous Communication**: Microservices communicate via **RabbitMQ** messages, ensuring loose coupling.  
- **Event-Driven Architecture**: Uses **MassTransit** to handle events (e.g., order placement, notifications).  
- **Email Webhooks**: Automatically trigger emails (via **MailKit+MimeKit**) on specific events (e.g., order confirmation).  
- **Dockerized**: Ready-to-run containers for RabbitMQ and services.  
- **Resilient & Scalable**: Fault-tolerant messaging with retries and queues.  

---

### 🛠 Tech Stack  
- **Backend**: ASP.NET Core 8 Web API  
- **Message Broker**: RabbitMQ  
- **Messaging Library**: MassTransit  
- **Email**: MailKit + MimeKit  
- **Containerization**: Docker  

---

### 📦 Getting Started  

#### Prerequisites  
- .NET 8 SDK  
- Docker (for RabbitMQ)  

#### Installation  
1. Clone the repo:  
   ```bash  
   git clone https://github.com/yourusername/async-microservices-demo.git
2. Start RabbitMQ via Docker:  
   ```bash  
   docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management  
3. Run the microservices:  
   ```bash  
   cd OrderService && dotnet run  
   cd NotificationService && dotnet run

---

### 🖥 How It Works

1. Order Service:

   - Receives POST /orders requests
   - Publishes OrderCreated event via MassTransit

2. Notification Service:

   - Consumes OrderCreated events from RabbitMQ
   - Sends emails using MailKit
  
