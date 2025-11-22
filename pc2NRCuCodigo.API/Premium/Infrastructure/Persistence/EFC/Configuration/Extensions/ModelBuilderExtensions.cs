using Microsoft.EntityFrameworkCore;
using pc2NRCuCodigo.API.Premium.Domain.Model.Aggregates;
using pc2NRCuCodigo.API.Premium.Domain.Model.ValueObjects;

namespace pc2NRCuCodigo.API.Premium.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPremiumOrderConfiguration(this ModelBuilder builder)
    {
        builder.Entity<PremiumOrder>().HasKey(x => x.Id);
        builder.Entity<PremiumOrder>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PremiumOrder>().Property(x => x.CustomerEmail).IsRequired().HasMaxLength(40);
        builder.Entity<PremiumOrder>().Property(e => e.ShippingType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .HasColumnType("varchar(32)")
            .IsRequired();
    }
}