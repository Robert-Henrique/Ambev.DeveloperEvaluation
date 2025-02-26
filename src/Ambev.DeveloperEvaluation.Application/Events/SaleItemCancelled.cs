namespace Ambev.DeveloperEvaluation.Application.Events;

public record SaleCancelled(Guid SaleId, decimal TotalAmount);