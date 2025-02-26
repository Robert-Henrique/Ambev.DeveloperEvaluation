using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.DiscountRules;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IDiscountStrategyFactory _discountStrategyFactory;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="discountStrategyFactory">The DiscountStrategyFactory instance</param>
    /// <param name="userRepository">The user repository instance</param>
    /// <param name="publishEndpoint">The publish Endpoint instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, 
        IMapper mapper, 
        IDiscountStrategyFactory discountStrategyFactory, 
        IUserRepository userRepository, 
        IPublishEndpoint publishEndpoint)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _discountStrategyFactory = discountStrategyFactory;
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
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

        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {command.UserId} not found");

        var sale = MapSale(command, user);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);

        await PublishEvent(sale);

        return result;
    }

    /// <summary>
    /// Publishes a SaleCreated event with the given sale details.
    /// </summary>
    /// <param name="sale">The sale entity containing the sale ID and total amount.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task PublishEvent(Sale sale) => await _publishEndpoint.Publish(new SaleCreated(sale.Id, sale.TotalAmount));

    /// <summary>
    /// Maps the CreateSaleCommand to a Sale entity, initializing necessary properties.
    /// </summary>
    /// <param name="command">The command containing the sale information.</param>
    /// <returns>A new instance of <see cref="Sale"/> with the mapped data.</returns>
    private Sale MapSale(CreateSaleCommand command, User user)
    {
        //TODO: Num cenário real deveria obter Branch e Product via repositório e validar a obrigatoriedade de cada entidade envolvida na venda
        var number = Guid.NewGuid();
        var customer = new ExternalIdentity(user.Id, user.Username);
        var branch = new ExternalIdentity(Guid.NewGuid(), command.BranchName);
        var items = command.Items.Select(MapSaleItem).ToList();

        return new Sale(number, customer, branch, items);
    }

    private SaleItem MapSaleItem(CreateSaleItem i)
    {
        var product = new ExternalIdentity(Guid.NewGuid(), i.ProductName);
        var unitPrice = new Price(i.UnitPrice);
        var discount = CalculateDiscount(i.Quantity, i.UnitPrice);
        return new SaleItem(product, i.Quantity, unitPrice, discount);
    }

    private decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        var strategy = _discountStrategyFactory.GetStrategy(quantity);
        return strategy.CalculateDiscount(quantity, unitPrice);
    }
}