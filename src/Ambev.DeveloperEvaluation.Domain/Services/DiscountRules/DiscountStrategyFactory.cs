namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;

public class DiscountStrategyFactory : IDiscountStrategyFactory
{
    private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

    public DiscountStrategyFactory(IEnumerable<IDiscountStrategy> discountStrategies)
    {
        _discountStrategies = discountStrategies;
    }

    public IDiscountStrategy GetStrategy(int quantity)
    {
        var discountStrategy = _discountStrategies.FirstOrDefault(d => d.Apply(quantity));

        if (discountStrategy == null)
            throw new InvalidOperationException("No discount strategy was applied.");

        return discountStrategy;
    }
}