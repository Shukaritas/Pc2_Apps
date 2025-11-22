using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Model.Queries;
using pc2NRCuCodigo.API.Premium.Domain.Repositories;
using pc2NRCuCodigo.API.Premium.Domain.Services;

namespace pc2NRCuCodigo.API.Premium.Application.Internal.QueryServices;

public class PremiumOrderQueryService(IPremiumOrderRepository premiumOrderRepository) : IPremiumOrderQueryService
{
    public async Task<IEnumerable<PremiumOrder>?> Handle(GetAllPremiumOrdersQuery query)
    {
        return await premiumOrderRepository.ListAsync();
    }
    
    public async Task<PremiumOrder?> Handle(GetPremiumOrdersByIdQuery query)
    {
        return await premiumOrderRepository.FindByIdAsync(query.orderId);
    }
}