using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class PedidoRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public PedidoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;

        public async Task<IEnumerable<Pedido>> ObterTodosOsPedidos() => 
            await _contexto.Pedidos
                .Include($"{Pedido.ProdutosPedidoField}.{nameof(ProdutoPedido.Produto)}")
                .Include($"{Pedido.ProdutosPedidoField}.{nameof(ProdutoPedido.AdicionaisProdutoPedido)}.{nameof(AdicionalProdutoPedido.Adicional)}")
                .ToListAsync();

        public async Task AdicionarPedido(Pedido pedido)
        {
            pedido.DataCriação = DateTime.Now;
            _contexto.Add(pedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task ObterPedido(int id) => await _contexto.Pedidos.SingleAsync(x => x.Id == id);
    }
}