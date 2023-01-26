using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class NacionalidadEntityTypeConfiguration : IEntityTypeConfiguration<Nacionalidad>
    {
        public void Configure(EntityTypeBuilder<Nacionalidad> NacionalidadConfiguration)
        {
            NacionalidadConfiguration.ToTable("Nacionalidad", ReferenciasContext.DEFAULT_SCHEMA);

            NacionalidadConfiguration.HasKey(o => o.Id);

            NacionalidadConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}