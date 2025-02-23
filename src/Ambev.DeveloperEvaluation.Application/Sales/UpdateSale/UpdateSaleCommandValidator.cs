using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand that defines validation rules for sale update command.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerName: Required, length between 3 and 50 characters
    /// - BranchName: Required
    /// - Items: Required
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.BranchName).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();
    }
}