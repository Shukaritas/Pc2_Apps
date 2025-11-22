using pc2NRCuCodigo.API.Premium.Domain.Model.Commands;
using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;

namespace pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;

public partial class PremiumOrder : PremiumOrderAudit
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; }
    public int ProductId { get; set; }
    public EShippingType ShippingType { get; set; }
    public string Werehouse { get; set; }
    
    public PremiumOrder(string customerEmail, int productId, EShippingType shippingType)
    {
        CustomerEmail = customerEmail;
        ProductId = productId;
        ShippingType = shippingType;
        Werehouse = Random.Shared.Next(1, 2) switch
        {
            1 => "Verdadero",
            _ => "Falso"
        };
    }

    public PremiumOrder(CreatePremiumOrderCommand command)
    {
        CustomerEmail = command.customerEmail;
        ProductId = command.productId;
        ShippingType = command.shippingType;
        Werehouse = Random.Shared.Next(1, 2) switch
        {
            1 => "Verdadero",
            _ => "Falso"
        };
    }
    
    public PremiumOrder()
    {
        CustomerEmail = string.Empty;
        ProductId = -1;
        ShippingType = EShippingType.NONE;
        Werehouse = Random.Shared.Next(1, 2) switch
        {
            1 => "Verdadero",
            _ => "Falso"
        };
    }
}