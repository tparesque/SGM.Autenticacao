using SGM.Autenticacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGM.Autenticacao.Repository.EntityConfig
{
    public class MunicipioConfig : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipio");

            builder.HasKey(x => x.MunicipioId);
            builder.Property(x => x.MunicipioId).ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.UF)
                .HasColumnType("varchar(2)")
                .IsRequired();

            builder.HasOne(x => x.Estado);
        }
    }
}
