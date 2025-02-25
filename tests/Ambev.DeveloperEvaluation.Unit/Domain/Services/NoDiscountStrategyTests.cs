using Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

public class NoDiscountStrategyTests
{
    private readonly NoDiscountStrategy _strategy;

    public NoDiscountStrategyTests()
    {
        _strategy = new NoDiscountStrategy();
    }

    [Fact]
    public void Apply_ShouldReturnTrue_WhenQuantityIsLessThanFour()
    {
        var quantity = 3;

        var result = _strategy.Apply(quantity);

        result.Should().BeTrue();
    }

    [Fact]
    public void Apply_ShouldReturnFalse_WhenQuantityIsFourOrMore()
    {
        var quantity = 4;

        var result = _strategy.Apply(quantity);

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(1, 10.0, 10.0)]
    [InlineData(2, 15.0, 30.0)]
    [InlineData(3, 20.0, 60.0)]
    public void CalculateDiscount_ShouldReturnTotalPriceWithoutDiscount(int quantity, decimal unitPrice, decimal expectedTotal)
    {
        var total = _strategy.CalculateDiscount(quantity, unitPrice);

        total.Should().Be(expectedTotal);
    }
}