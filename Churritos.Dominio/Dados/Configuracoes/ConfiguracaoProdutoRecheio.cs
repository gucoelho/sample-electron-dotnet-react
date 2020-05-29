using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoProdutoRecheio : IEntityTypeConfiguration<ProdutoRecheio>
    {
        public void Configure(EntityTypeBuilder<ProdutoRecheio> builder)
        {
            builder.ToTable("ProdutoRecheio");
            builder.HasKey(o => new { o.ProdutoId, o.RecheioId });
        }
    }
}