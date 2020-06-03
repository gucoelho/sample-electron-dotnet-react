﻿// <auto-generated />
using System;
using Churritos.Dominio.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Churritos.Dominio.Migrations
{
    [DbContext(typeof(ContextoDaAplicação))]
    partial class ContextoDaAplicaçãoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Churritos.Dominio.Modelos.Adicional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Adicional");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Coco",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Confete",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Granulado",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Granulado colorido",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Choco ball",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Amendoim moído",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 7,
                            Nome = "Oreo",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 8,
                            Nome = "Kit kat preto",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 9,
                            Nome = "Kit kat branco",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 10,
                            Nome = "Ouro branco",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 11,
                            Nome = "Sonho de valsa",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 12,
                            Nome = "Ovomantine",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 13,
                            Nome = "Ninho em pó",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 14,
                            Nome = "Morango",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 15,
                            Nome = "Banana",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 16,
                            Nome = "Cheddar",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 17,
                            Nome = "Catupiry",
                            Tipo = "Cobertura",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 18,
                            Nome = "Cream cheese",
                            Tipo = "Cobertura",
                            Valor = 2m
                        },
                        new
                        {
                            Id = 19,
                            Nome = "Doce de leite",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 20,
                            Nome = "Chocolate",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 21,
                            Nome = "Goiabada",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 22,
                            Nome = "Misto",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 23,
                            Nome = "Nutella",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 24,
                            Nome = "Ninho",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 25,
                            Nome = "Nutella com ninho",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 26,
                            Nome = "Frango",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 27,
                            Nome = "Calabresa",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 28,
                            Nome = "Carne moída",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 29,
                            Nome = "Carne seca",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 30,
                            Nome = "Pizza",
                            Tipo = "Recheio",
                            Valor = 0m
                        },
                        new
                        {
                            Id = 31,
                            Nome = "Bacon",
                            Tipo = "Extra",
                            Valor = 2m
                        },
                        new
                        {
                            Id = 32,
                            Nome = "Teste",
                            Tipo = "Extra",
                            Valor = 2.34m
                        });
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categoria");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Churros"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Bebidas"
                        });
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.EntidadesAuxiliares.AdicionalProduto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdicionalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProdutoId", "AdicionalId");

                    b.HasIndex("AdicionalId");

                    b.ToTable("AdicionalProduto");

                    b.HasData(
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 19
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 20
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 21
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 22
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 1
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 2
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 3
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 4
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 5
                        },
                        new
                        {
                            ProdutoId = 1,
                            AdicionalId = 6
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 26
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 27
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 28
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 29
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 30
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 31
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 32
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 16
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 17
                        },
                        new
                        {
                            ProdutoId = 4,
                            AdicionalId = 18
                        });
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.EntidadesAuxiliares.AdicionalProdutoPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AdicionalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoPedidoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AdicionalId");

                    b.HasIndex("ProdutoPedidoId");

                    b.ToTable("AdicionalProdutoPedido");
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCriação")
                        .HasColumnName("DataCriacao")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoriaId = 1,
                            Nome = "Churros Doce Tradicional",
                            Valor = 8m
                        },
                        new
                        {
                            Id = 2,
                            CategoriaId = 1,
                            Nome = "Churros Doce Especial",
                            Valor = 12m
                        },
                        new
                        {
                            Id = 3,
                            CategoriaId = 1,
                            Nome = "Churros Doce Gelado",
                            Valor = 16m
                        },
                        new
                        {
                            Id = 4,
                            CategoriaId = 1,
                            Nome = "Churros Salgado",
                            Valor = 10m
                        },
                        new
                        {
                            Id = 5,
                            CategoriaId = 2,
                            Nome = "Coca-Cola Lata 269ml",
                            Valor = 4.99m
                        },
                        new
                        {
                            Id = 6,
                            CategoriaId = 2,
                            Nome = "Guaraná Lata 269ml",
                            Valor = 4.99m
                        },
                        new
                        {
                            Id = 7,
                            CategoriaId = 2,
                            Nome = "Suco Dell Valle 250ml",
                            Valor = 4.50m
                        });
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.ProdutoPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoPedido");
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.EntidadesAuxiliares.AdicionalProduto", b =>
                {
                    b.HasOne("Churritos.Dominio.Modelos.Adicional", "Adicional")
                        .WithMany()
                        .HasForeignKey("AdicionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Churritos.Dominio.Modelos.Produto", "Produto")
                        .WithMany("AdicionaisProduto")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.EntidadesAuxiliares.AdicionalProdutoPedido", b =>
                {
                    b.HasOne("Churritos.Dominio.Modelos.Adicional", "Adicional")
                        .WithMany()
                        .HasForeignKey("AdicionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Churritos.Dominio.Modelos.ProdutoPedido", "Produto")
                        .WithMany("AdicionaisProdutoPedido")
                        .HasForeignKey("ProdutoPedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.Produto", b =>
                {
                    b.HasOne("Churritos.Dominio.Modelos.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Churritos.Dominio.Modelos.ProdutoPedido", b =>
                {
                    b.HasOne("Churritos.Dominio.Modelos.Pedido", null)
                        .WithMany("_produtos")
                        .HasForeignKey("PedidoId");

                    b.HasOne("Churritos.Dominio.Modelos.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");
                });
#pragma warning restore 612, 618
        }
    }
}
