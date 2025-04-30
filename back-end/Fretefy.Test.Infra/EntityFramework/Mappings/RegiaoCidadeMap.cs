using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
    {
        public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
        {
            builder.HasKey(rc => new { rc.RegiaoId, rc.CidadeId });

            builder
                .HasOne(r => r.Regiao)
                .WithMany(p => p.RegiaoCidades)
                .HasForeignKey(rc => rc.RegiaoId);

            builder
                .HasOne(c => c.Cidade)
                .WithMany(p => p.RegiaoCidades)
                .HasForeignKey(rc => rc.CidadeId);
        }
    }
}
