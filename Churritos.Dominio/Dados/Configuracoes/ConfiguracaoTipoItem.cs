using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoTipoItem : IEntityTypeConfiguration<TipoItem>
    {
        public void Configure(EntityTypeBuilder<TipoItem> builder)
        {
            builder.ToTable("TipoItem");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome);

            builder.Ignore(x => x.Coberturas);
            builder.Ignore(x => x.Recheios);

            builder.HasMany<TipoItemCobertura>(TipoItem.TipoItemCoberturasField)
                .WithOne()
                .HasForeignKey("TipoItemId");
            
            builder.HasMany<TipoItemRecheio>(TipoItem.TipoItemRecheiosField)
                .WithOne()
                .HasForeignKey("TipoItemId");
        }
    }
}