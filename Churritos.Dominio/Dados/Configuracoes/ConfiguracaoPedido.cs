using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoPedido : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DataCriação)
                .HasColumnName("DataCriacao");
            
            builder.HasMany(Pedido.ProdutosPedidoField)
                .WithOne()
                .HasForeignKey("PedidoId");
            
            builder.Ignore(x => x.Produtos);
        }
    }
}