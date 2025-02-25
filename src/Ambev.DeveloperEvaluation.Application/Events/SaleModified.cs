namespace Ambev.DeveloperEvaluation.Application.Events;

public record SaleModified(Guid SaleId, decimal TotalAmount);