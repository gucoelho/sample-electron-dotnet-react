using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoProdutoCobertura : IEntityTypeConfiguration<ProdutoCobertura>
    {
        public void Configure(EntityTypeBuilder<ProdutoCobertura> builder)
        {
            builder.ToTable("ProdutoCobertura");
            builder.HasKey(o => new { o.ProdutoId, o.CoberturaId });
        }
    }
}