using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Infrastructure.EntityConfigurations
{
    class SectorEntityTypeConfiguration : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> SectorConfiguration)
        {
            SectorConfiguration.ToTable("Sector", TareasContext.DEFAULT_SCHEMA);

            SectorConfiguration.HasKey(o => o.Id);

            SectorConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}