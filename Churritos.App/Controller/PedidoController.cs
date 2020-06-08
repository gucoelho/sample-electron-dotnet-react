using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepositório _repositorio;
        private readonly ProdutoRepositório _produtoRepositório;
        private readonly AdicionalRepositório _adicionalRepositório;

        public PedidoController(PedidoRepositório repositorio,
            ProdutoRepositório produtoRepositório,
            AdicionalRepositório adicionalRepositório)
        {
            _repositorio = repositorio;
            _produtoRepositório = produtoRepositório;
            _adicionalRepositório = adicionalRepositório;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoViewModel>> Get()
        {
            var pedidos = await _repositorio.ObterTodosOsPedidos();

            return pedidos.Select(x => new PedidoViewModel
            {
                Id = x.Id,
                Quantidade = x.Produtos.Count,
                DataCriacao = x.DataCriação,
                Valor = x.ValorTotal
            });
        }

        [HttpPost]
        public async Task Post(PedidoDTO pedidoDto)
        {
            var pedido = new Pedido()
            {
                Desconto = pedidoDto.Desconto
            };

            foreach (var item in pedidoDto.Itens)
            {
                var produto = _produtoRepositório.ObterProdutoPorId(item.ProdutoId);
                var produtoPedido = new ProdutoPedido
                {
                    Produto = produto,
                    Valor = produto.Valor
                };

                if (item.Adicionais?.Length > 0)
                {
                    var vinculoAdicionais = item.Adicionais.Select(x => new AdicionalProdutoPedido
                    {
                        Produto = produtoPedido,
                        Adicional = produto.Adicionais.Single(y => y.Id == x.Id)
                    }).ToList();

                    produtoPedido.AdicionaisProdutoPedido = vinculoAdicionais;
                }

                pedido.AdicionarProdutoPedido(produtoPedido);
            }

            await _repositorio.AdicionarPedido(pedido);
        }

        [HttpGet("download")]
        public async Task<IEnumerable<PedidoDownloadViewModel>> GetPedidosDownload()
        {
            var pedidos = await _repositorio.ObterTodosOsPedidos();

            var download = pedidos
                .SelectMany(pedido =>
                {
                    var pedidoDownload = pedido.Produtos
                        .SelectMany(produto =>
                        {
                            var listaDeRetorno = new List<PedidoDownloadViewModel>
                            {
                                new PedidoDownloadViewModel
                                {
                                    PedidoId = pedido.Id,
                                    Data = pedido.DataCriação,
                                    ProdutoId = produto.Id,
                                    NomeProduto = produto.Nome,
                                    Valor = produto.Valor
                                }
                            };

                            if (pedido.Adicionais[produto].Any())
                                listaDeRetorno.AddRange(pedido.Adicionais[produto]
                                    .Select(adicional =>
                                        new PedidoDownloadViewModel
                                        {
                                            PedidoId = pedido.Id,
                                            Data = pedido.DataCriação,
                                            ProdutoId = produto.Id,
                                            NomeProduto = produto.Nome,
                                            Valor = adicional.Valor,
                                            AdicionalId = adicional.Id,
                                            AdicionalNome = adicional.Nome
                                        }));

                            return listaDeRetorno;
                        }).ToList();
                    if(pedido.Desconto > 0)
                        pedidoDownload.Add(new PedidoDownloadViewModel
                        {
                            Data = pedido.DataCriação,
                            Valor = - pedido.Desconto,
                            PedidoId = pedido.Id,
                            NomeProduto = "Desconto"
                        });

                    return pedidoDownload;
                });

            return download.ToList();
        }
    }

    public class PedidoDownloadViewModel
    {
        public int PedidoId { get; set; }
        public DateTime Data { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int AdicionalId { get; set; }
        public string AdicionalNome { get; set; }
        public decimal Valor { get; set; }
    }

    public class PedidoViewModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class ItemPedidoViewModel
    {
        public int ProdutoId { get; set; }
        public AdicionalViewModel[] Adicionais { get; set; }
    }

    public class PedidoDTO
    {
        public IEnumerable<ItemPedidoViewModel> Itens { get; set; }
        public decimal Desconto { get; set; }
    }

    public class AdicionalViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}