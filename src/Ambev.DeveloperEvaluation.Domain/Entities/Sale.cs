using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

/// <summary>
/// Represents a sale in the system with sale items.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Unique identifier for the sale.
    /// </summary>
    public Guid Number { get; private set; }

    /// <summary>
    /// The date when the sale was created.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// The customer associated with the sale.
    /// </summary>
    public ExternalIdentity Customer { get; private set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public ExternalIdentity Branch { get; private set; }

    /// <summary>
    /// List of items included in the sale.
    /// </summary>
    public IList<SaleItem> Items { get; private set; }

    /// <summary>
    /// The total amount of the sale, calculated as the sum of the total price of all items.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    /// <summary>
    /// The current status of the sale.
    /// </summary>
    public SaleStatus Status { get; private set; }

    private Sale() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sale"/> class with the specified details.
    /// This constructor ensures that the sale entity is always valid upon creation.
    /// </summary>
    /// <param name="number">The unique sale identifier.</param>
    /// <param name="customer">The customer who made the purchase.</param>
    /// <param name="branch">The branch where the sale took place.</param>
    /// <param name="items">The list of items included in the sale.</param>
    /// <exception cref="ValidationException">Thrown if the sale data is invalid.</exception>
    public Sale(Guid number, ExternalIdentity customer, ExternalIdentity branch, IList<SaleItem> items)
    {
        Number = number;
        Date = DateTime.UtcNow;
        Customer = customer;
        Branch = branch;
        Items = items;
        Status = SaleStatus.Active;

        Validate();
    }

    /// <summary>
    /// Updates the sale's customer, branch, and status.
    /// </summary>
    /// <param name="customer">The new customer associated with the sale.</param>
    /// <param name="branch">The new branch where the sale is recorded.</param>
    /// <param name="status">The new status of the sale.</param>
    public void Update(ExternalIdentity customer, ExternalIdentity branch, SaleStatus status)
    {
        Customer = customer;
        Branch = branch;
        Status = status;

        Validate();
    }

    /// <summary>
    /// Changes the status of a specific sale item.
    /// </summary>
    /// <param name="itemId">The unique identifier of the sale item to update.</param>
    /// <param name="status">The new status to apply to the sale item.</param>
    public void ChangeStatus(Guid itemId, SaleItemStatus status)
    {
        var item = Items.FirstOrDefault(f => f.Id == itemId);
        item?.Update(status);
    }

    /// <summary>
    /// Validates the current sale instance against business rules.
    /// If the validation fails, a <see cref="ValidationException"/> is thrown.
    /// </summary>
    /// <exception cref="ValidationException">Thrown if the sale entity is invalid.</exception>
    private void Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}