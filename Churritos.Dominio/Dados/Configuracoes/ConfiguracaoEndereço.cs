using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoEndereço : IEntityTypeConfiguration<Endereço>
    {
        public void Configure(EntityTypeBuilder<Endereço> builder)
        {
            builder.ToTable("Endereco");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Endereços)
                .HasForeignKey(x => x.ClienteId);
        }
    }
}