namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The name of the purchasing customer
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The name of the branch where the sale occurred
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The list of items involved in the sale
    /// </summary>
    public IList<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>(0);
}