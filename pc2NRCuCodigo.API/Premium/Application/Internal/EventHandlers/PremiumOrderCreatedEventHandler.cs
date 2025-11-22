using pc2NRCuCodigo.API.Premium.Domain.Model.Events;
using pc2NRCuCodigo.API.Shared.Application.Internal.EventHandlers;

namespace pc2NRCuCodigo.API.Premium.Application.Internal.EventHandlers;

public class PremiumOrderCreatedEventHandler : IEventHandler<PremiumOrdersCreatedEvent>
{
    public Task Handle(PremiumOrdersCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    private static Task On(PremiumOrdersCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Premium Order with email: {0}", domainEvent.CustomerEmail);
        return Task.CompletedTask;
    }
}