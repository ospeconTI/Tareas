using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class LocalidadEntityTypeConfiguration : IEntityTypeConfiguration<Localidad>
    {
        public void Configure(EntityTypeBuilder<Localidad> LocalidadConfiguration)
        {
            LocalidadConfiguration.ToTable("Localidad", ReferenciasContext.DEFAULT_SCHEMA);

            LocalidadConfiguration.HasKey(o => o.Id);

            LocalidadConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}