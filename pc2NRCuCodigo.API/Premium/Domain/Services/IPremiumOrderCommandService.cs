using pc2NRCuCodigo.API.Premium.Domain.Model.Commands;
using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;

namespace pc2NRCuCodigo.API.Premium.Domain.Services;

public interface IPremiumOrderCommandService
{
    Task<PremiumOrder?> Handle(CreatePremiumOrderCommand command);
}