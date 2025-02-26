namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItem
{
    /// <summary>
    /// The name of the product in the sale.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product purchased in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product at the time of the sale.
    /// </summary>
    public decimal UnitPrice { get; set; }
}