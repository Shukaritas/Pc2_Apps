using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Model.Queries;

namespace pc2NRCuCodigo.API.Premium.Domain.Services;

public interface IPremiumOrderQueryService
{
    Task<IEnumerable<PremiumOrder>?> Handle(GetAllPremiumOrdersQuery query);
    Task<PremiumOrder?> Handle(GetPremiumOrdersByIdQuery query);
}