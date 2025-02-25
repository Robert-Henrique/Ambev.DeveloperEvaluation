using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Helpers;

public class SaleItemBuilder
{
    private ExternalIdentity _product;
    private int _quantity;
    private Price _unitPrice;
    private decimal _discount;

    public SaleItemBuilder()
    {
        var faker = new Faker();
        _product = ExternalIdentityBuilder.AnExternalIdentity().Build();
        _quantity = 3;
        _unitPrice = new Price(faker.Finance.Amount());
        _discount = 0;
    }
    
    public static SaleItemBuilder ASaleItem() => new SaleItemBuilder();

    public SaleItemBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public SaleItemBuilder WithUnitPrice(decimal unitPrice)
    {
        _unitPrice = new Price(unitPrice);
        return this;
    }

    public SaleItem Build() => new SaleItem(_product, _quantity, _unitPrice, _discount);
}