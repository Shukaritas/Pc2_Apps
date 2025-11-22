using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Repositories;
using pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace pc2NRCuCodigo.API.Premium.Infrastructure.Persistence.EFC.Repositories;

public class PremiumOrderRepository(AppDbContext ctx)
    : BaseRepository<PremiumOrder>(ctx), IPremiumOrderRepository;