using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Helpers;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void Should_Create_SaleItem_With_Valid_Data()
    {
        var product = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var quantity = 5;
        var unitPrice = new Price(10.0m);
        var discount = 2.0m;

        var saleItem = new SaleItem(product, quantity, unitPrice, discount);

        saleItem.Product.Should().Be(product);
        saleItem.Quantity.Should().Be(quantity);
        saleItem.UnitPrice.Should().Be(unitPrice);
        saleItem.Discount.Should().Be(discount);
        saleItem.TotalPrice.Should().Be((unitPrice.Value * quantity) - discount);
    }

    [Fact]
    public void Should_Throw_Exception_When_Quantity_Exceeds_Limit()
    {
        var product = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var unitPrice = new Price(10.0m);
        var discount = 2.0m;
        var invalidQuantity = 25;

        Action act = () => new SaleItem(product, invalidQuantity, unitPrice, discount);

        act.Should().Throw<ValidationException>()
            .WithMessage("*No item can have a quantity greater than 20.*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Product_Is_Null()
    {
        ExternalIdentity? product = null;
        var quantity = 5;
        var unitPrice = new Price(10.0m);
        var discount = 2.0m;
        
        Action act = () => new SaleItem(product!, quantity, unitPrice, discount);

        act.Should().Throw<ValidationException>()
            .Where(ex => ex.Errors.Any(e => e.PropertyName == "Product"));
    }

    [Fact]
    public void Should_Throw_Exception_When_UnitPrice_Is_Null()
    {
        var product = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var quantity = 5;
        Price? unitPrice = null;
        var discount = 2.0m;
        
        Action act = () => new SaleItem(product, quantity, unitPrice!, discount);
        
        act.Should().Throw<ValidationException>()
            .Where(ex => ex.Errors.Any(e => e.PropertyName == "UnitPrice"));
    }

    [Fact]
    public void Should_Calculate_TotalPrice_Correctly()
    {
        var product = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var quantity = 3;
        var unitPrice = new Price(15.0m);
        var discount = 5.0m;
        var expectedTotalPrice = (unitPrice.Value * quantity) - discount;

        var saleItem = new SaleItem(product, quantity, unitPrice, discount);
        
        saleItem.TotalPrice.Should().Be(expectedTotalPrice);
    }
}