namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public class TwentyPercentDiscountStrategy : IDiscountStrategy
{
    private static decimal TwentyPercent => 0.20m;

    public bool Apply(int quantity) => quantity is >= 10 and <= 20;

    public decimal CalculateDiscount(int quantity, decimal unitPrice) => unitPrice * quantity * TwentyPercent;
}