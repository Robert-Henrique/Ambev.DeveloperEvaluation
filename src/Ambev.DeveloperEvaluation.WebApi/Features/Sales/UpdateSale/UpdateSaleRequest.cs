﻿using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
public class UpdateSaleRequest
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
    public List<UpdateSaleItemRequest> Items { get; set; }
}