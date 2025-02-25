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
    /// - UserId: Required
    /// - BranchName: Required
    /// - Items: Required
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.UserId).NotEmpty();
        RuleFor(sale => sale.BranchName).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();
    }
}