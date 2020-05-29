using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoProduto : IEntityTypeConfiguration<Produto>
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
            
            builder.Ignore(x => x.Coberturas);
            builder.Ignore(x => x.Recheios);

            builder.HasMany(x => x.ProdutoCoberturas)
                .WithOne(x => x.Produto)
                .HasForeignKey(x => x.ProdutoId);
            
            builder.HasMany(x => x.ProdutoRecheios)
                .WithOne(x => x.Produto)
                .HasForeignKey(x => x.ProdutoId);
        }
    }
}