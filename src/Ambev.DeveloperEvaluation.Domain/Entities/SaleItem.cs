using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale, including product details, pricing, and discounts.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// The product associated with this sale item.
    /// </summary>
    public ExternalIdentity Product { get; private set; }

    /// <summary>
    /// The quantity of the product being sold.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public Price UnitPrice { get; private set; }

    /// <summary>
    /// The discount applied to this sale item.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// The total price of the sale item, calculated as (Unit Price * Quantity) - Discount.
    /// </summary>
    public decimal TotalPrice => UnitPrice.Value * Quantity - Discount;

    /// <summary>
    /// The current status of the sale item.
    /// </summary>
    public SaleItemStatus Status { get; private set; }

    private SaleItem() { }

    public SaleItem(ExternalIdentity product, int quantity, Price unitPrice, decimal discount)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        Status = SaleItemStatus.Active;

        Validate();
    }

    /// <summary>
    /// Updates the status of the sale item.
    /// </summary>
    /// <param name="status">The new status to be assigned to the sale item.</param>
    public void Update(SaleItemStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Validates the current sale instance against business rules.
    /// If the validation fails, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <exception cref="ValidationException">Thrown if the sale entity is invalid.</exception>
    private void Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}