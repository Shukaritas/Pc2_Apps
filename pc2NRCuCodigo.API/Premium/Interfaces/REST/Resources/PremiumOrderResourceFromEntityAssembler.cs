using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;

namespace pc2NRCuCodigo.API.Premium.Interfaces.REST.Resources;

public static class PremiumOrderResourceFromEntityAssembler
{
    public static PremiumOrderResource ToResourceFromEntity(PremiumOrder premiumOrder)
    {
        return new PremiumOrderResource(
            premiumOrder.Id,
            premiumOrder.CustomerEmail,
            premiumOrder.ProductId,
            premiumOrder.ShippingType,
            premiumOrder.CreatedDate
        );
    }
}