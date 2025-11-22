using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;
using pc2NRCuCodigo.API.Shared.Domain.Model.Events;

namespace pc2NRCuCodigo.API.Premium.Domain.Model.Events;

public class PremiumOrdersCreatedEvent(string customerEmail, int productId, EShippingType shippingType) : IEvent
{
    public string CustomerEmail { get; } = customerEmail;
    public int ProductId { get; } = productId;
    public EShippingType ShippingType { get; } = shippingType;
}