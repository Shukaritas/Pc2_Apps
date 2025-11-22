using pc2NRCuCodigo.API.Premium.Domain.Model.Commands;
using pc2NRCuCodigo.API.Premium.Interfaces.REST.Resources;

namespace pc2NRCuCodigo.API.Premium.Interfaces.REST.Transform;

public static class CreatePremiumOrderCommandFromResourceAssembler
{
    public static CreatePremiumOrderCommand ToCommandFromResource(CreatePremiumOrderResource command)
    {
        return new CreatePremiumOrderCommand(command.customerEmail, command.productId, command.shippingType);
    }
}