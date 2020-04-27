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
            CriarCategoria(builder);
        }

        private void CriarSeedCobertura(ModelBuilder builder)
        {
            builder.Entity<Cobertura>().HasData(
                new Cobertura
                {
                    Id = 1,
                    Nome = "Coco"
                },
                new Cobertura
                {
                    Id = 2,
                    Nome = "Confete"
                },
                new Cobertura
                {
                    Id = 3,
                    Nome = "Granulado"
                },
                new Cobertura
                {
                    Id = 4,
                    Nome = "Granulado colorido"
                },
                new Cobertura
                {
                    Id = 5,
                    Nome = "Choco ball"
                },
                new Cobertura
                {
                    Id = 6,
                    Nome = "Amendoim moído"
                },
                new Cobertura
                {
                    Id = 7,
                    Nome = "Oreo"
                },
                new Cobertura
                {
                    Id = 8,
                    Nome = "Kit kat preto"
                },
                new Cobertura
                {
                    Id = 9,
                    Nome = "Kit kat branco"
                },
                new Cobertura
                {
                    Id = 10,
                    Nome = "Ouro branco"
                },
                new Cobertura
                {
                    Id = 11,
                    Nome = "Sonho de valsa"
                },
                new Cobertura
                {
                    Id = 12,
                    Nome = "Ovomantine"
                },
                new Cobertura
                {
                    Id = 13,
                    Nome = "Ninho em pó"
                },
                new Cobertura
                {
                    Id = 14,
                    Nome = "Morango"
                },
                new Cobertura
                {
                    Id = 15,
                    Nome = "Banana"
                },
                new Cobertura
                {
                    Id = 16,
                    Nome = "Cheddar"
                },
                new Cobertura
                {
                    Id = 17,
                    Nome = "Catupiry"
                },
                new Cobertura
                {
                    Id = 18,
                    Nome = "Cream cheese"
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
                },
                new Recheio
                {
                    Id = 3,
                    Nome = "Goiabada"
                },
                new Recheio
                {
                    Id = 4,
                    Nome = "Misto"
                },
                new Recheio
                {
                    Id = 5,
                    Nome = "Nutella"
                },
                new Recheio
                {
                    Id = 6,
                    Nome = "Ninho"
                },
                new Recheio
                {
                    Id = 7,
                    Nome = "Nutella com ninho"
                },
                new Recheio
                {
                    Id = 8,
                    Nome = "Frango"
                },
                new Recheio
                {
                    Id = 9,
                    Nome = "Calabresa"
                },
                new Recheio
                {
                    Id = 10,
                    Nome = "Carne moída"
                },
                new Recheio
                {
                    Id = 11,
                    Nome = "Carne seca"
                },
                new Recheio
                {
                    Id = 12,
                    Nome = "Pizza"
                }
            );
        }
        private void CriarCategoria(ModelBuilder builder)
        {
            builder.Entity<Categoria>().HasData(
                new Categoria
                {
                    Id = 1,
                    Nome = "Doces Tradicionais"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Doces Especiais"
                },
                new Categoria
                {
                    Id = 3,
                    Nome = "Doces Gelados"
                },
                new Categoria
                {
                    Id = 4,
                    Nome = "Salgados"
                }
            );
        }
    }
}