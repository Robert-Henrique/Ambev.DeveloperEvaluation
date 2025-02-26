using Ambev.DeveloperEvaluation.Application.Events;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleModifiedConsumer : IConsumer<SaleModified>
{
    public async Task Consume(ConsumeContext<SaleModified> context)
    {
        Console.WriteLine($"Sale Modified: {context.Message.SaleId}, Amount: {context.Message.TotalAmount}");
    }
}