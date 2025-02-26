using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler responsible for processing UpdateSaleCommand requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="publishEndpoint">The MassTransit publish endpoint instance.</param>
    public UpdateSaleHandler(ISaleRepository saleRepository,
        IMapper mapper,
        IUserRepository userRepository,
        IPublishEndpoint publishEndpoint)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request.
    /// </summary>
    /// <param name="command">The update sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated sale details.</returns>
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
        UpdateSaleItems(command, sale);

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        var result = _mapper.Map<UpdateSaleResult>(updatedSale);

        await PublishSaleUpdatedEvent(sale);

        return result;
    }

    /// <summary>
    /// Publishes an event when a sale is updated.
    /// </summary>
    /// <param name="sale">The updated sale entity.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task PublishSaleUpdatedEvent(Sale sale) =>
        await _publishEndpoint.Publish(new SaleModified(sale.Id, sale.TotalAmount));

    /// <summary>
    /// Publishes an event when a sale is cancelled.
    /// </summary>
    /// <param name="sale">The cancelled sale entity.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task PublishSaleCancelledEvent(Sale sale) =>
        await _publishEndpoint.Publish(new SaleCancelled(sale.Id, sale.TotalAmount));

    /// <summary>
    /// Publishes an event when a sale item is cancelled.
    /// </summary>
    /// <param name="updateSaleItem">The cancelled sale item.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task PublishSaleItemCancelledEvent(UpdateSaleItem updateSaleItem) =>
        await _publishEndpoint.Publish(new SaleItemCancelled(updateSaleItem.Id));

    /// <summary>
    /// Updates the status of sale items and publishes events if necessary.
    /// </summary>
    /// <param name="command">The update sale command containing item details.</param>
    /// <param name="sale">The sale entity to update.</param>
    private async void UpdateSaleItems(UpdateSaleCommand command, Sale sale)
    {
        foreach (var item in command.Items)
        {
            sale.ChangeStatus(item.Id, item.Status);

            if (item.Status == SaleItemStatus.Canceled)
                await PublishSaleItemCancelledEvent(item);
        }
    }

    /// <summary>
    /// Updates the sale details and publishes events if necessary.
    /// </summary>
    /// <param name="command">The update sale command.</param>
    /// <param name="sale">The sale entity to update.</param>
    /// <param name="user">The user associated with the sale.</param>
    private async void UpdateSale(UpdateSaleCommand command, Sale sale, User user)
    {
        var customer = new ExternalIdentity(user.Id, user.Username);
        var branch = new ExternalIdentity(Guid.NewGuid(), command.BranchName);
        sale.Update(customer, branch, command.Status);

        if (sale.Status == SaleStatus.Canceled)
            await PublishSaleCancelledEvent(sale);
    }
}