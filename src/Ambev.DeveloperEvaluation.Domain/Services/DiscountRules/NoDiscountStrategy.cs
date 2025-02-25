namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public class NoDiscountStrategy : IDiscountStrategy
{
    public bool Apply(int quantity) => quantity < 4;

    public decimal CalculateDiscount(int quantity, decimal unitPrice) => unitPrice * quantity;
}