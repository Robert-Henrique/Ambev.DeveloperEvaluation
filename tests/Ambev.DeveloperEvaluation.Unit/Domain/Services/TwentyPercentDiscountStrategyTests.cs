using Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class TwentyPercentDiscountStrategyTests
{
    private readonly TwentyPercentDiscountStrategy _strategy;

    public TwentyPercentDiscountStrategyTests()
    {
        _strategy = new TwentyPercentDiscountStrategy();
    }

    [Theory]
    [InlineData(9, false)]
    [InlineData(10, true)]
    [InlineData(15, true)]
    [InlineData(20, true)]
    [InlineData(21, false)]
    public void Apply_ShouldReturnExpectedResult(int quantity, bool expectedResult)
    {
        var result = _strategy.Apply(quantity);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(10, 10, 20)]
    [InlineData(15, 20, 60)]
    [InlineData(20, 50, 200)]
    public void CalculateDiscount_ShouldReturnCorrectDiscount(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        var discount = _strategy.CalculateDiscount(quantity, unitPrice);

        discount.Should().Be(expectedDiscount);
    }
}