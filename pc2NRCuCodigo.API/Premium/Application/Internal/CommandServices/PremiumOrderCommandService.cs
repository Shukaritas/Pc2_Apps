using pc2NRCuCodigo.API.Premium.Domain.Model.Commands;
using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Model.Events;
using pc2NRCuCodigo.API.Premium.Domain.Repositories;
using pc2NRCuCodigo.API.Shared.Domain.Repositories;
using Cortex.Mediator;
using pc2NRCuCodigo.API.Premium.Domain.Services;

namespace pc2NRCuCodigo.API.Premium.Application.Internal.CommandServices;

public class PremiumOrderCommandService(IPremiumOrderRepository premiumOrderRepository,
    IUnitOfWork unitOfWork, IMediator domainEventPublisher) : IPremiumOrderCommandService
{
    public async Task<PremiumOrder?> Handle(CreatePremiumOrderCommand command)
    {
        var premiumOrder = new PremiumOrder(command);
        await premiumOrderRepository.AddAsync(premiumOrder);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new PremiumOrdersCreatedEvent(premiumOrder.CustomerEmail, premiumOrder.ProductId, premiumOrder.ShippingType));
        return premiumOrder;
    }
}