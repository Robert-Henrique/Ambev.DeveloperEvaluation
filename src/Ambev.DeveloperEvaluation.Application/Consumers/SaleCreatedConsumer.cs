using Ambev.DeveloperEvaluation.Application.Events;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleCreatedConsumer : IConsumer<SaleCreated>
{
    public async Task Consume(ConsumeContext<SaleCreated> context)
    {
        Console.WriteLine($"Sale created: {context.Message.SaleId}, Amount: {context.Message.TotalAmount}");
    }
}