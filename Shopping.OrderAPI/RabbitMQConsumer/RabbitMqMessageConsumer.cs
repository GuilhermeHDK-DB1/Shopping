﻿using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shopping.OrderAPI.Messages;
using Shopping.OrderAPI.Model;
using Shopping.OrderAPI.Repository;

namespace Shopping.OrderAPI.RabbitMQConsumer;

public class RabbitMqMessageConsumer : BackgroundService
{
    private readonly OrderRepository _repository;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqMessageConsumer(OrderRepository repository)
    {
        _repository = repository;
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "CheckoutQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (channel, evt) =>
        {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            CheckoutHeaderVo vo = JsonSerializer.Deserialize<CheckoutHeaderVo>(content);
            ProcessOrder(vo).GetAwaiter().GetResult();
            _channel.BasicAck(deliveryTag: evt.DeliveryTag, multiple: false);
        };
        
        _channel.BasicConsume(
            queue: "CheckoutQueue",
            autoAck: false,
            consumer: consumer);
        
        return Task.CompletedTask;
    }

    private async Task ProcessOrder(CheckoutHeaderVo vo)
    {
        OrderHeader order = new()
        {
            UserId = vo.UserId,
            FirstName = vo.FirstName,
            LastName = vo.LastName,
            OrderDetails = new List<OrderDetail>(),
            CardNumber = vo.CardNumber,
            CouponCode = vo.CouponCode,
            CVV = vo.CVV,
            DiscountAmount = vo.DiscountAmount,
            Email = vo.Email,
            ExpiryMonthYear = vo.ExpiryMonthYear,
            OrderTime = DateTime.Now,
            PurchaseAmount = vo.PurchaseAmount,
            PaymentStatus = false,
            Phone = vo.Phone,
            DateTime = vo.Time
        };

        foreach (var details in vo.CartDetails)
        {
            OrderDetail detail = new()
            {
                ProductId = details.ProductId,
                ProductName = details.Product.Name,
                Price = details.Product.Price,
                Count = details.Count,
            };
            order.CartTotalItens += details.Count;
            order.OrderDetails.Add(detail);
        }

        await _repository.AddOrderAsync(order);
    }
}