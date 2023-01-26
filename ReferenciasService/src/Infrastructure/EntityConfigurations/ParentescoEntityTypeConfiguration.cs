using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class ParentescoEntityTypeConfiguration : IEntityTypeConfiguration<Parentesco>
    {
        public void Configure(EntityTypeBuilder<Parentesco> ParentescoConfiguration)
        {
            ParentescoConfiguration.ToTable("Parentesco", ReferenciasContext.DEFAULT_SCHEMA);

            ParentescoConfiguration.HasKey(o => o.Id);

            ParentescoConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}