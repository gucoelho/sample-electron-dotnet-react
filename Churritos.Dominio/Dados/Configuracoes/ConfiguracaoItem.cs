using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoItem : IEntityTypeConfiguration<Produto>
    {
        
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(x => x.Valor);
            
            builder.HasMany<ProdutoPedido>()
                .WithOne(x => x.Produto)
                .HasForeignKey("ProdutoId");
        }
    }
}