using Ambev.DeveloperEvaluation.Application.Events;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleItemCancelledConsumer : IConsumer<SaleItemCancelled>
{
    public async Task Consume(ConsumeContext<SaleItemCancelled> context)
    {
        Console.WriteLine($"Sale item cancelled: {context.Message.SaleItemId}");
    }
}