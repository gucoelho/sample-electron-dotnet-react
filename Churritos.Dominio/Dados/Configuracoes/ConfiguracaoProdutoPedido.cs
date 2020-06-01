using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Churritos.Dominio.Dados.Configuracoes
{
    public class ConfiguracaoProdutoPedido : IEntityTypeConfiguration<ProdutoPedido>
    {
        public void Configure(EntityTypeBuilder<ProdutoPedido> builder)
        {
            builder.ToTable("ProdutoPedido");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Ignore(x => x.Adicionais);

            builder.HasOne(x => x.Produto).WithMany()
                .HasForeignKey("ProdutoId");
        }
    }
}