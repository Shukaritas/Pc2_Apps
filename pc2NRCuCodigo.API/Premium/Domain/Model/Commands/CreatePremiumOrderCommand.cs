using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;

namespace pc2NRCuCodigo.API.Premium.Domain.Model.Commands;

public record CreatePremiumOrderCommand(string customerEmail, int productId, EShippingType shippingType);