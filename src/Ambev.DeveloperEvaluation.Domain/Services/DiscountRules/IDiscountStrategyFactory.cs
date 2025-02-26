namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public interface IDiscountStrategyFactory
{
    IDiscountStrategy GetStrategy(int quantity);
}