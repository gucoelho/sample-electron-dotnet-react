using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoCategoriaCobertura : IEntityTypeConfiguration<CategoriaCobertura>
    {
        public void Configure(EntityTypeBuilder<CategoriaCobertura> builder)
        {
            builder.ToTable("CategoriaCobertura");
            
            builder.HasOne<Categoria>();
            builder.HasOne<Cobertura>();
            
            builder.HasKey("CategoriaId", "CoberturaId");
        }
    }
}