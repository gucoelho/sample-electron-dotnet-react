using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoItem : IEntityTypeConfiguration<Item>
    {
        
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(x => x.Valor);
            
            builder.HasMany<ItemPedido>()
                .WithOne(x => x.Item)
                .HasForeignKey("ItemId");
        }
    }
}