using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = MapSale(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }

    /// <summary>
    /// Maps the CreateSaleCommand to a Sale entity, initializing necessary properties.
    /// </summary>
    /// <param name="command">The command containing the sale information.</param>
    /// <returns>A new instance of <see cref="Sale"/> with the mapped data.</returns>
    private static Sale MapSale(CreateSaleCommand command)
    {
        //TODO: Num cenário real deveria obter Customer, Branch e Product via repositório e validar a obrigatoriedade de cada entidade envolvida na venda

        var sale = new Sale
        {
            Number = Guid.NewGuid().ToString(),
            Date = DateTime.UtcNow,
            Customer = new ExternalIdentity(Guid.NewGuid(), command.CustomerName),
            Branch = new ExternalIdentity(Guid.NewGuid(), command.BranchName),
            Items = command.Items.Select(i => new SaleItem
            {
                Product = new ExternalIdentity(Guid.NewGuid(), i.ProductName),
                Quantity = i.Quantity,
                UnitPrice = new Price(i.UnitPrice)
            }).ToList()
        };
        return sale;
    }
}