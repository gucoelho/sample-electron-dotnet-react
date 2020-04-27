using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoCobertura : IEntityTypeConfiguration<Cobertura>
    {
        public void Configure(EntityTypeBuilder<Cobertura> builder)
        {
            builder.ToTable("Cobertura");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome);

            builder.HasMany<CategoriaCobertura>()
                .WithOne()
                .HasForeignKey("CoberturaId");
        }
    }
}