using Ambev.DeveloperEvaluation.Application.Events;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

public class SaleCancelledConsumer : IConsumer<SaleCancelled>
{
    public async Task Consume(ConsumeContext<SaleCancelled> context)
    {
        Console.WriteLine($"Sale cancelled: {context.Message.SaleId}, Amount: {context.Message.TotalAmount}");
    }
}