using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Number).NotEmpty();
        RuleFor(sale => sale.Customer).NotEmpty();
        RuleFor(sale => sale.Branch).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty();
    }
}