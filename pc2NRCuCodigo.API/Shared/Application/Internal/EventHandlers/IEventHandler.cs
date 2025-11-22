using pc2NRCuCodigo.API.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace pc2NRCuCodigo.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}