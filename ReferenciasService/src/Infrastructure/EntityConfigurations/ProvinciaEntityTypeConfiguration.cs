using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class ProvinciaEntityTypeConfiguration : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> ProvinciaConfiguration)
        {
            ProvinciaConfiguration.ToTable("Provincia", ReferenciasContext.DEFAULT_SCHEMA);

            ProvinciaConfiguration.HasKey(o => o.Id);

            ProvinciaConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}