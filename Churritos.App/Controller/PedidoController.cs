using System;
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
                var produtoPedido = new ProdutoPedido
                {
                    Produto = produto,
                    Valor = produto.Valor
                };
                
                if (produto.Categoria.Nome == "Churros")
                {
                    var cobertura = _adicionalRepositório.ObterCoberturaPorId(item.IdCobertura);
                    var recheio = _adicionalRepositório.ObterRecheioPorId(item.IdRecheio);

                    produtoPedido.AdicionaisProdutoPedido = new List<AdicionalProdutoPedido>
                    {
                        new AdicionalProdutoPedido
                        {
                            Produto = produtoPedido,
                            Adicional = recheio,
                        },
                        new AdicionalProdutoPedido
                        {
                            Produto = produtoPedido,
                            Adicional = cobertura,
                        }
                    };    
                }
                pedido.AdicionarProdutoPedido(produtoPedido); 
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