﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// The unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The UserId of the purchasing customer
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The name of the branch for the sale.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The status of the sale.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// The list of items to be updated for the sale.
    /// </summary>
    public IList<UpdateSaleItem> Items { get; set; } = new List<UpdateSaleItem>(0);

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}