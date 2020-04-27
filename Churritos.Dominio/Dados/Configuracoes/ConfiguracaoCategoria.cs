using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoCategoria : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome);

            builder.Ignore(x => x.Coberturas);
            builder.Ignore(x => x.Recheios);

            builder.HasMany<CategoriaCobertura>(Categoria.CategoriaCoberturasField)
                .WithOne()
                .HasForeignKey("CategoriaId");
            
            builder.HasMany<CategoriaRecheio>(Categoria.CategoriaRecheiosField)
                .WithOne()
                .HasForeignKey("CategoriaId");
        }
    }
}