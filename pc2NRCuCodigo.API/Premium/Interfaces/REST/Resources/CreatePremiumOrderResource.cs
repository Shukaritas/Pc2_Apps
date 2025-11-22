using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;

namespace pc2NRCuCodigo.API.Premium.Interfaces.REST.Resources;

public record CreatePremiumOrderResource(string customerEmail, int productId, EShippingType shippingType);