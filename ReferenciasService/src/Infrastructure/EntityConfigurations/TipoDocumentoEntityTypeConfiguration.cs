using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class TipoDocumentoEntityTypeConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> TipoDocumentoConfiguration)
        {
            TipoDocumentoConfiguration.ToTable("TipoDocumento", ReferenciasContext.DEFAULT_SCHEMA);

            TipoDocumentoConfiguration.HasKey(o => o.Id);

            TipoDocumentoConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}