using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(saleItem => saleItem.Product).NotEmpty();
        RuleFor(saleItem => saleItem.Quantity).NotEmpty();
        RuleFor(saleItem => saleItem.UnitPrice).NotEmpty();
        RuleFor(saleItem => saleItem.Quantity)
            .LessThanOrEqualTo(20)
            .WithMessage("No item can have a quantity greater than 20.");
    }
}