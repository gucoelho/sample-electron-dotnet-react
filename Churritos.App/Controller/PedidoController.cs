using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
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

        public PedidoController(PedidoRepositório repositorio, ProdutoRepositório produtoRepositório)
        {
            _repositorio = repositorio;
            _produtoRepositório = produtoRepositório;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoViewModel>> Get()
        {
            var pedidos= await _repositorio.ObterTodosOsPedidos();

            return pedidos.Select(x => new PedidoViewModel
            {
                Id = x.Id,
                Quantidade = x.Produtos.Count,
                DataCriacao = x.DataCriação,
                Valor = x.ValorTotal
            });
        }

        [HttpPost]
        public async Task Post(IEnumerable<ItemPedidoViewModel> itens)
        {
            var pedido = new Pedido()
            {
                DataCriação = DateTime.Now,
            };

            foreach (var item in itens)
            {
                var produto = _produtoRepositório.ObterProdutoPorId(item.IdProduto);
                pedido.AdicionarProdutoPedido(new ProdutoPedido
                {
                    Produto = produto,
                    Valor = item.Valor,
                    // TODO: Adicionar Coberturas
                }); 
            }

            await _repositorio.AdicionarPedido(pedido);
        }
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
        public int IdProduto { get; set; }
        public int IdCobertura { get; set; }
        public int IdRecheio { get; set; }
        public int Valor { get; set; }
    }
}