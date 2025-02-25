using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="userRepository">The UserRepository instance</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, 
        IMapper mapper, 
        IUserRepository userRepository)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="command">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {command.UserId} not found");

        UpdateSale(command, sale, user);
        UpdateItems(command, sale);

        var createdSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        var result = _mapper.Map<UpdateSaleResult>(createdSale);
        return result;
    }

    private static void UpdateItems(UpdateSaleCommand command, Sale sale)
    {
        foreach (var item in command.Items)
            sale.ChangeStatus(item.Id, item.Status);
    }

    private static void UpdateSale(UpdateSaleCommand command, Sale sale, User user)
    {
        var customer = new ExternalIdentity(user.Id, user.Username);
        var branch = new ExternalIdentity(Guid.NewGuid(), command.BranchName);
        sale.Update(customer, branch, command.Status);
    }
}