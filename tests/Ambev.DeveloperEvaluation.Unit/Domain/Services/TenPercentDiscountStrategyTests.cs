using Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class TenPercentDiscountStrategyTests
{
    private readonly TenPercentDiscountStrategy _strategy;

    public TenPercentDiscountStrategyTests()
    {
        _strategy = new TenPercentDiscountStrategy();
    }

    [Theory]
    [InlineData(3, false)]
    [InlineData(4, true)]
    [InlineData(9, true)]
    [InlineData(10, false)]
    public void Apply_ShouldReturnExpectedResult(int quantity, bool expectedResult)
    {
        var result = _strategy.Apply(quantity);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(4, 10, 4)]
    [InlineData(5, 20, 10)]
    [InlineData(9, 15, 13.5)]
    public void CalculateDiscount_ShouldReturnCorrectDiscount(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        var discount = _strategy.CalculateDiscount(quantity, unitPrice);

        discount.Should().Be(expectedDiscount);
    }
}