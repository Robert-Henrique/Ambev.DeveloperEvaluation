namespace Ambev.DeveloperEvaluation.Application.Events;

public record SaleCreated(Guid SaleId, decimal TotalAmount);