using System.Collections.Generic;
using System.Linq;
using Churritos.Dominio.Dados.Configuracoes;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Dados
{
    public class ContextoDaAplicação : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Adicional> Adicionais { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
 
        public ContextoDaAplicação(DbContextOptions<ContextoDaAplicação> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConfiguracaoCategoria());
            builder.ApplyConfiguration(new ConfiguracaoAdicional());
            builder.ApplyConfiguration(new ConfiguracaoProduto());
            builder.ApplyConfiguration(new ConfiguracaoAdicionalProduto());
            builder.ApplyConfiguration(new ConfiguracaoAdicionalProdutoPedido());
            builder.ApplyConfiguration(new ConfiguracaoPedido());
            builder.ApplyConfiguration(new ConfiguracaoProdutoPedido());

            var coberturas = CriarSeedAdicionaisDeCobertura(builder);
            var recheios = CriarSeedRecheio(builder);
            var categorias = CriarCategoria(builder);
            var produtos = CriarSeedProdutos(builder);

            SeedAdicionaisChurrosDoceTradicional(builder);
            SeedAdicionaisChurrosSalgado(builder);
        }

        private static void SeedAdicionaisChurrosSalgado(ModelBuilder builder)
        {
            builder.Entity<AdicionalProduto>().HasData(
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 26
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 27
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 28
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 29
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 30
                }
            );

            builder.Entity<AdicionalProduto>().HasData(
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 16
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 17
                },
                new AdicionalProduto
                {
                    ProdutoId = 4,
                    AdicionalId = 18
                }
            );
        }

        private static void SeedAdicionaisChurrosDoceTradicional(ModelBuilder builder)
        {
            builder.Entity<AdicionalProduto>().HasData(
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 19
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 20
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 21
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 22
                }
            );

            builder.Entity<AdicionalProduto>().HasData(
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 1
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 2
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 3
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 4
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 5
                },
                new AdicionalProduto
                {
                    ProdutoId = 1,
                    AdicionalId = 6
                }
            );
        }

        private List<Produto> CriarSeedProdutos(ModelBuilder builder)
        {
            var produtos = new List<Produto>
            {
                new Produto
                {
                    Id = 1,
                    Nome = "Churros Doce Tradicional",
                    CategoriaId = 1,
                    Valor = 8,

                },
                new Produto
                {
                    Id = 2,
                    Nome = "Churros Doce Especial",
                    CategoriaId = 1,
                    Valor = 12
                },
                new Produto
                {
                    Id = 3,
                    CategoriaId = 1,
                    Nome = "Churros Doce Gelado",
                    Valor = 16
                },
                new Produto
                {
                    Id = 4,
                    CategoriaId = 1,
                    Nome = "Churros Salgado",
                    Valor = 10
                },
                new Produto
                {
                    Id = 5,
                    CategoriaId = 2,
                    Nome = "Coca-Cola Lata 269ml",
                    Valor = 4.99m
                },
                new Produto
                {
                    Id = 6,
                    CategoriaId = 2,
                    Nome = "Guaraná Lata 269ml",
                    Valor = 4.99m
                },
                new Produto
                {
                    Id = 7,
                    CategoriaId = 2,
                    Nome = "Suco Dell Valle 250ml",
                    Valor = 4.50m
                }
            };
            builder.Entity<Produto>().HasData(
                produtos
            );

            return produtos;
        }
        private List<Adicional> CriarSeedAdicionaisDeCobertura(ModelBuilder builder)
        {
            var coberturas = new List<Adicional>
            {
                new Adicional
                {
                    Id = 1,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Coco"
                },
                new Adicional
                {
                    Id = 2,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Confete"
                },
                new Adicional
                {
                    Id = 3,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Granulado"
                },
                new Adicional
                {
                    Id = 4,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Granulado colorido"
                },
                new Adicional
                {
                    Id = 5,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Choco ball"
                },
                new Adicional
                {
                    Id = 6,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Amendoim moído"
                },
                new Adicional
                {
                    Id = 7,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Oreo"
                },
                new Adicional
                {
                    Id = 8,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Kit kat preto"
                },
                new Adicional
                {
                    Id = 9,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Kit kat branco"
                },
                new Adicional
                {
                    Id = 10,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Ouro branco"
                },
                new Adicional
                {
                    Id = 11,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Sonho de valsa"
                },
                new Adicional
                {
                    Id = 12,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Ovomantine"
                },
                new Adicional
                {
                    Id = 13,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Ninho em pó"
                },
                new Adicional
                {
                    Id = 14,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Morango"
                },
                new Adicional
                {
                    Id = 15,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Banana"
                },
                new Adicional
                {
                    Id = 16,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Cheddar"
                },
                new Adicional
                {
                    Id = 17,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Catupiry"
                },
                new Adicional
                {
                    Id = 18,
                    Tipo = TipoAdicional.Cobertura,
                    Nome = "Cream cheese",
                    Valor = 2
                }
            };
            builder.Entity<Adicional>().HasData(coberturas);
            return coberturas;
        }
        private List<Adicional> CriarSeedRecheio(ModelBuilder builder)
        {
            var recheios = new List<Adicional>
            {
                new Adicional
                {
                    Id = 19,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Doce de leite"
                },
                new Adicional
                {
                    Id = 20,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Chocolate"
                },
                new Adicional
                {
                    Id = 21,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Goiabada"
                },
                new Adicional
                {
                    Id = 22,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Misto"
                },
                new Adicional
                {
                    Id = 23,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Nutella"
                },
                new Adicional
                {
                    Id = 24,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Ninho"
                },
                new Adicional
                {
                    Id = 25,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Nutella com ninho"
                },
                new Adicional
                {
                    Id = 26,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Frango"
                },
                new Adicional
                {
                    Id = 27,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Calabresa"
                },
                new Adicional
                {
                    Id = 28,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Carne moída"
                },
                new Adicional
                {
                    Id = 29,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Carne seca"
                },
                new Adicional
                {
                    Id = 30,
                    Tipo = TipoAdicional.Recheio,
                    Nome = "Pizza"
                },
                new Adicional
                {
                    Id = 31,
                    Tipo = TipoAdicional.Extra,
                    Nome = "Bacon",
                    Valor = 2
                }
            };
            builder.Entity<Adicional>().HasData(recheios);
            return recheios;
        }
        private List<Categoria> CriarCategoria(ModelBuilder builder)
        {
            var categorias = new List<Categoria>()
            {
                new Categoria
                {
                    Id = 1,
                    Nome = "Churros"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Bebidas"
                }
            };
            builder.Entity<Categoria>().HasData(categorias);
            return categorias;
        }
    }
}