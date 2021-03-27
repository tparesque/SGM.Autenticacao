using SGM.Autenticacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGM.Autenticacao.Repository.EntityConfig
{
    public class EnderecoConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(x => x.EnderecoId);

            builder.Property(x => x.Logradouro)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Numero)                
                .IsRequired();

            builder.Property(x => x.CEP)
                .HasColumnType("varchar(8)")
                .IsRequired();

            builder.Property(x => x.Bairro)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(x => x.Complemento)
               .HasColumnType("varchar(150)");

            builder.HasOne(x => x.Municipio);
        }
    }
}
