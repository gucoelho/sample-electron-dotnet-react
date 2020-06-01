using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoAdicionalProduto : IEntityTypeConfiguration<AdicionalProduto>
    {
        public void Configure(EntityTypeBuilder<AdicionalProduto> builder)
        {
            builder.ToTable("AdicionalProduto");

            builder.HasKey(o => new {o.ProdutoId, o.AdicionalId});


            builder.HasOne(x => x.Adicional)
                .WithMany()
                .HasForeignKey(x => x.AdicionalId);


            builder.HasOne(x => x.Produto)
                .WithMany(x => x.AdicionaisProduto)
                .HasForeignKey(x => x.ProdutoId);
        }
    }
}