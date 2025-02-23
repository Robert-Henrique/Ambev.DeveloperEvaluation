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

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
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

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

        UpdateSale(command, existingSale);
        UpdateItems(command, existingSale);

        var createdSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);
        var result = _mapper.Map<UpdateSaleResult>(createdSale);
        return result;
    }

    private static void UpdateItems(UpdateSaleCommand command, Sale existingSale)
    {
        foreach (var item in command.Items)
        {
            var existingItem = existingSale.Items.FirstOrDefault(f => f.Id == item.Id);

            if (existingItem != null)
                existingItem.Status = item.Status;
        }
    }

    private static void UpdateSale(UpdateSaleCommand command, Sale existingSale)
    {
        existingSale.Customer = new ExternalIdentity(Guid.NewGuid(), command.CustomerName);
        existingSale.Branch = new ExternalIdentity(Guid.NewGuid(), command.BranchName);
        existingSale.Date = DateTime.UtcNow;
        existingSale.Status = command.Status;
    }
}