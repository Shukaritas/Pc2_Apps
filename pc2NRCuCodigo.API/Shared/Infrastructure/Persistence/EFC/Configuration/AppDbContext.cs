using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using pc2NRCuCodigo.API.Premium.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //Bounded Context
        builder.ApplyPremiumOrderConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
