using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: Required
    /// - BranchName: Required
    /// - Items: Required
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.UserId).NotEmpty();
        RuleFor(sale => sale.BranchName).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();
    }
}