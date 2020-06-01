using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoAdicional : IEntityTypeConfiguration<Adicional>
    {
        public void Configure(EntityTypeBuilder<Adicional> builder)
        {
            builder.ToTable("Adicional");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome);

            builder.Property(x => x.Tipo)
                .HasConversion(new EnumToStringConverter<TipoAdicional>());
        }
    }
}