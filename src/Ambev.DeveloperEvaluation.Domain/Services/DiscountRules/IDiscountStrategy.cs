namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public interface IDiscountStrategy
{
    bool Apply(int quantity);

    decimal CalculateDiscount(int quantity, decimal unitPrice);
}