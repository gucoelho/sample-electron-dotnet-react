using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoTipoItemRecheio : IEntityTypeConfiguration<TipoItemRecheio>
    {
        public void Configure(EntityTypeBuilder<TipoItemRecheio> builder)
        {
            builder.ToTable("TipoItemRecheio");
            
            builder.HasKey("TipoItemId", "RecheioId");
        }
    }
}