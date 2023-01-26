using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class EstadoCivilEntityTypeConfiguration : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> EstadoCivilConfiguration)
        {
            EstadoCivilConfiguration.ToTable("EstadoCivil", ReferenciasContext.DEFAULT_SCHEMA);

            EstadoCivilConfiguration.HasKey(o => o.Id);

            EstadoCivilConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}