using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoCategoriaRecheio : IEntityTypeConfiguration<CategoriaRecheio>
    {
        public void Configure(EntityTypeBuilder<CategoriaRecheio> builder)
        {
            builder.ToTable("CategoriaRecheio");
            
            builder.HasOne<Categoria>();
            builder.HasOne<Recheio>();
            
            builder.HasKey("CategoriaId", "RecheioId");
        }
    }
}