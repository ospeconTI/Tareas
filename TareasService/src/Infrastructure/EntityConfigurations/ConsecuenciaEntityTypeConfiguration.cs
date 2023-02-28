using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Infrastructure.EntityConfigurations
{
    class ConsecuenciaEntityTypeConfiguration : IEntityTypeConfiguration<Consecuencia>
    {
        public void Configure(EntityTypeBuilder<Consecuencia> ConsecuenciaConfiguration)
        {
            ConsecuenciaConfiguration.ToTable("Consecuencia", TareasContext.DEFAULT_SCHEMA);

            ConsecuenciaConfiguration.HasKey(o => o.Id);

            ConsecuenciaConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}