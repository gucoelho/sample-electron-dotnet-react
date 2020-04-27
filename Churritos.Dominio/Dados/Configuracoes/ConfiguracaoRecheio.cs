using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoRecheio : IEntityTypeConfiguration<Recheio>
    {
        public void Configure(EntityTypeBuilder<Recheio> builder)
        {
            builder.ToTable("Recheio");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome);

            builder.HasMany<CategoriaRecheio>()
                .WithOne()
                .HasForeignKey("RecheioId");
        }
    }
}