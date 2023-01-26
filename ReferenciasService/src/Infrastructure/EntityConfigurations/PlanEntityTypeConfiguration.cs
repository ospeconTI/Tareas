using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Infrastructure.EntityConfigurations
{
    class PlanEntityTypeConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> PlanConfiguration)
        {
            PlanConfiguration.ToTable("Plan", ReferenciasContext.DEFAULT_SCHEMA);

            PlanConfiguration.HasKey(o => o.Id);

            PlanConfiguration.Ignore(b => b.DomainEvents);

        }
    }
}