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
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Recheio> Recheios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ContextoDaAplicação(DbContextOptions<ContextoDaAplicação> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConfiguracaoCategoria());
            builder.ApplyConfiguration(new ConfiguracaoCobertura());
            builder.ApplyConfiguration(new ConfiguracaoRecheio());
            builder.ApplyConfiguration(new ConfiguracaoProduto());
            builder.ApplyConfiguration(new ConfiguracaoProdutoCobertura());
            builder.ApplyConfiguration(new ConfiguracaoProdutoRecheio());
            builder.ApplyConfiguration(new ConfiguracaoPedido());
            builder.ApplyConfiguration(new ConfiguracaoProdutoPedido());
            
            var coberturas = CriarSeedCobertura(builder);
            var recheios = CriarSeedRecheio(builder);
            var categorias = CriarCategoria(builder);
            var produtos = CriarSeedProdutos(builder);
            VincularProdutosComRecheiosECoberturas(builder, coberturas, recheios, produtos);
        }

        private void VincularProdutosComRecheiosECoberturas(ModelBuilder builder, List<Cobertura> coberturas, List<Recheio> recheios, List<Produto> produtos)
        {
            var coberturasDosProdutos = produtos.SelectMany(
                x => coberturas.Select(y => new ProdutoCobertura
            {
                ProdutoId = x.Id,
                CoberturaId = y.Id
            }));
            
            builder.Entity<ProdutoCobertura>().HasData(coberturasDosProdutos);
            
            var recheiosDosProdutos = produtos.SelectMany(
                x => recheios.Select(y => new ProdutoRecheio()
            {
                ProdutoId = x.Id,
                RecheioId = y.Id
            }));
            
            builder.Entity<ProdutoRecheio>().HasData(recheiosDosProdutos);
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
                    CategoriaId = 2,
                    Valor = 12
                },
                new Produto
                {
                    Id = 3,
                    CategoriaId = 3,
                    Nome = "Churros Doce Gelado",
                    Valor = 16
                },
                new Produto
                {
                    Id = 4,
                    CategoriaId = 4,
                    Nome = "Churros Salgado",
                    Valor = 10
                },
                new Produto
                {
                    Id = 5,
                    CategoriaId = 5,
                    Nome = "Churros Salgado Especial",
                    Valor = 12
                }
            };
            builder.Entity<Produto>().HasData(
                produtos
            );

            return produtos;
        }
        private List<Cobertura> CriarSeedCobertura(ModelBuilder builder)
        {
            var coberturas = new List<Cobertura>
            {
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
            };
            builder.Entity<Cobertura>().HasData(coberturas);
            return coberturas;
        }
        private List<Recheio> CriarSeedRecheio(ModelBuilder builder)
        {
            var recheios = new List<Recheio>
            {
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
            };
            builder.Entity<Recheio>().HasData(recheios);
            return recheios;
        }
        private List<Categoria> CriarCategoria(ModelBuilder builder)
        {
            var categorias = new List<Categoria>()
            {
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
                },
                new Categoria
                {
                    Id = 5,
                    Nome = "Salgados Especiais"
                }
            };
            builder.Entity<Categoria>().HasData(categorias);
            return categorias;
        }
    }
}