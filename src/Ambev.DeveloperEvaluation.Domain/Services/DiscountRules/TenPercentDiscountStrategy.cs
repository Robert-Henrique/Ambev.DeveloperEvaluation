namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public class TenPercentDiscountStrategy : IDiscountStrategy
{
    private static decimal TenPercent => 0.10m;

    public bool Apply(int quantity) => quantity is >= 4 and < 10;

    public decimal CalculateDiscount(int quantity, decimal unitPrice) => unitPrice * quantity * TenPercent;
}