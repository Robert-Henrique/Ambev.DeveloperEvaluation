using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

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
    public ExternalIdentity Product { get; set; }

    /// <summary>
    /// The quantity of the product being sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public Price UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to this sale item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total price of the sale item, calculated as (Unit Price * Quantity) - Discount.
    /// </summary>
    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => UnitPrice.Value * Quantity - Discount;
        private set => _totalPrice = value;
    }

    /// <summary>
    /// The current status of the sale item.
    /// </summary>
    public SaleItemStatus Status { get; set; } = SaleItemStatus.Active;

    /// <summary>
    /// Validates the SaleItem entity based on business rules.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}