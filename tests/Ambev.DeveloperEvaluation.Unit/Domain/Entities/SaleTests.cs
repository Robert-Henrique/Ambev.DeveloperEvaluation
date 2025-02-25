using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Helpers;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Sale_Should_Create_Valid_Instance()
    {
        var number = Guid.NewGuid();
        var customer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var branch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().WithQuantity(2).WithUnitPrice(10.0m).Build(),
            SaleItemBuilder.ASaleItem().WithQuantity(1).WithUnitPrice(15.0m).Build()
        };
        
        var sale = new Sale(number, customer, branch, items);
        
        Assert.Equal(number, sale.Number);
        Assert.Equal(customer, sale.Customer);
        Assert.Equal(branch, sale.Branch);
        Assert.Equal(items, sale.Items);
        Assert.Equal(35.0m, sale.TotalAmount);
        Assert.Equal(SaleStatus.Active, sale.Status);
        Assert.True(sale.Date <= DateTime.UtcNow);
    }

    [Fact]
    public void Sale_Should_Throw_ValidationException_When_Customer_Is_Empty()
    {
        var number = Guid.NewGuid();
        var branch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().WithQuantity(2).WithUnitPrice(10.0m).Build()
        };

        void Act() => new Sale(number, null, branch, items);

        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Sale_Should_Throw_ValidationException_When_Branch_Is_Empty()
    {
        var number = Guid.NewGuid();
        var customer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().WithQuantity(2).WithUnitPrice(10.0m).Build()
        };

        void Act() => new Sale(number, customer, null, items);

        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Sale_Should_Throw_ValidationException_When_Items_Are_Empty()
    {
        var number = Guid.NewGuid();
        var customer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var branch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>(0);
        
        void Act() => new Sale(number, customer, branch, items);

        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Update_Should_Update_Customer_Branch_And_Status()
    {
        var number = Guid.NewGuid();
        var initialCustomer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var initialBranch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().Build()
        };
        var sale = new Sale(number, initialCustomer, initialBranch, items);
        var newCustomer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var newBranch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var newStatus = SaleStatus.Canceled;
        
        sale.Update(newCustomer, newBranch, newStatus);
        
        Assert.Equal(newCustomer, sale.Customer);
        Assert.Equal(newBranch, sale.Branch);
        Assert.Equal(newStatus, sale.Status);
    }

    [Fact]
    public void Update_Should_Throw_ValidationException_When_Customer_Is_Null()
    {
        var number = Guid.NewGuid();
        var initialCustomer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var initialBranch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().Build()
        };
        var sale = new Sale(number, initialCustomer, initialBranch, items);
        var newBranch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var newStatus = SaleStatus.Canceled;

        void Act() => sale.Update(null, newBranch, newStatus);

        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void Update_Should_Throw_ValidationException_When_Branch_Is_Null()
    {
        var number = Guid.NewGuid();
        var initialCustomer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var initialBranch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var items = new List<SaleItem>
        {
            SaleItemBuilder.ASaleItem().Build()
        };
        var sale = new Sale(number, initialCustomer, initialBranch, items);
        var newCustomer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var newStatus = SaleStatus.Canceled;

        void Act() => sale.Update(newCustomer, null, newStatus);

        Assert.Throws<ValidationException>(Act);
    }

    [Fact]
    public void ChangeStatus_Should_Update_SaleItem_Status()
    {
        var number = Guid.NewGuid();
        var customer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var branch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var saleItem = SaleItemBuilder.ASaleItem().Build();
        var items = new List<SaleItem> { saleItem };
        var sale = new Sale(number, customer, branch, items);
        var newStatus = SaleItemStatus.Canceled;
        
        sale.ChangeStatus(saleItem.Id, newStatus);
        
        Assert.Equal(newStatus, saleItem.Status);
    }

    [Fact]
    public void ChangeStatus_Should_Not_Throw_When_Item_Not_Found()
    {
        var number = Guid.NewGuid();
        var customer = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var branch = ExternalIdentityBuilder.AnExternalIdentity().Build();
        var saleItem = SaleItemBuilder.ASaleItem().Build();
        var items = new List<SaleItem> { saleItem };
        var sale = new Sale(number, customer, branch, items);
        var nonExistentItemId = Guid.NewGuid();
        var newStatus = SaleItemStatus.Canceled;

        void Act() => sale.ChangeStatus(nonExistentItemId, newStatus);

        var exception = Record.Exception(Act);
        Assert.Null(exception);
    }
}