using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoTipoItemCobertura : IEntityTypeConfiguration<TipoItemCobertura>
    {
        public void Configure(EntityTypeBuilder<TipoItemCobertura> builder)
        {
            builder.ToTable("TipoItemCobertura");

            builder.HasKey("TipoItemId", "CoberturaId");
        }
    }
}