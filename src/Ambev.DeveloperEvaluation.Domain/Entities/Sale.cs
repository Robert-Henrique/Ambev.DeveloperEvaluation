using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system with sale items.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public string Number { get; set; } = DateTime.Now.ToLongTimeString();

    /// <summary>
    /// The date when the sale was created.
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The customer associated with the sale.
    /// </summary>
    public ExternalIdentity Customer { get; set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public ExternalIdentity Branch { get; set; }

    /// <summary>
    /// List of items included in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; }

    /// <summary>
    /// The total amount of the sale, calculated as the sum of the total price of all items.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    /// <summary>
    /// The current status of the sale.
    /// </summary>
    public SaleStatus Status { get; set; } = SaleStatus.Active;

    /// <summary>
    /// Validates the sale entity based on business rules.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}