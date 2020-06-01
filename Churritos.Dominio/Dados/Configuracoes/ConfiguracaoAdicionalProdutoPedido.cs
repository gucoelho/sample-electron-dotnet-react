using System.Diagnostics;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoAdicionalProdutoPedido : IEntityTypeConfiguration<AdicionalProdutoPedido>
    {
        public void Configure(EntityTypeBuilder<AdicionalProdutoPedido> builder)
        {
            builder.ToTable("AdicionalProdutoPedido");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            builder.HasOne(x => x.Adicional)
                .WithMany()
                .HasForeignKey(x => x.AdicionalId);

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.AdicionaisProdutoPedido)
                .HasForeignKey(x => x.ProdutoPedidoId);
        }
    }
}