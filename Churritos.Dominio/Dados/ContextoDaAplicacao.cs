using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Dados
{
    public class ContextoDaAplicação : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Recheio> Recheios { get; set; }
        public DbSet<Item> Itens { get; set; }

        public ContextoDaAplicação(DbContextOptions<ContextoDaAplicação> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ContextoDaAplicação).Assembly);
            CriarSeedCobertura(builder);
            CriarSeedRecheio(builder);
            CriarTipoItem(builder);
        }

        private void CriarSeedCobertura(ModelBuilder builder)
        {
            builder.Entity<Cobertura>().HasData(
                new Cobertura
                {
                    Id = 1,
                    Nome = "Confete"
                },
                new Cobertura
                {
                    Id = 2,
                    Nome = "Coco"
                }
            );
        }
        private void CriarSeedRecheio(ModelBuilder builder)
        {
            builder.Entity<Recheio>().HasData(
                new Recheio
                {
                    Id = 1,
                    Nome = "Doce de leite"
                },
                new Recheio
                {
                    Id = 2,
                    Nome = "Chocolate"
                }
            );
        }
        private void CriarTipoItem(ModelBuilder builder)
        {
            builder.Entity<TipoItem>().HasData(
                new TipoItem
                {
                    Id = 1,
                    Nome = "Doces Tradicionais"
                },
                new TipoItem
                {
                    Id = 2,
                    Nome = "Doces Especiais"
                },
                new TipoItem
                {
                    Id = 3,
                    Nome = "Doces Gelados"
                },
                new TipoItem
                {
                    Id = 4,
                    Nome = "Salgados"
                }
            );
        }
    }
}