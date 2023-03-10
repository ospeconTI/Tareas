using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Infrastructure.EntityConfigurations
{
    class TareaEntityTypeConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> TareaConfiguration)
        {
            TareaConfiguration.ToTable("Tarea", TareasContext.DEFAULT_SCHEMA);

            TareaConfiguration.HasKey(o => o.Id);

            TareaConfiguration.Ignore(b => b.DomainEvents);

            TareaConfiguration.Property(p => p.Estado).HasConversion(v => v.Nombre, v => EstadoTarea.FromName(v));

            TareaConfiguration.Property(p => p.Tipo).HasConversion(v => v.Nombre, v => TipoTarea.FromName(v));

        }
    }
}