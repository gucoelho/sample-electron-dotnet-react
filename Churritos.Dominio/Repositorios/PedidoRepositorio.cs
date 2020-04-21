using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class PedidoRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public PedidoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;

        public async Task<IEnumerable<Pedido>> ObterTodosOsPedidos() => await _contexto.Pedidos.ToListAsync();

        public async Task AdicionarItensNoPedido(IEnumerable<ItemPedido> itens, Pedido pedido)
        {
            foreach (var itemPedido in itens)
                pedido.AdicionarItemPedido(itemPedido);

            await _contexto.SaveChangesAsync();
        }

        public async Task ObterPedido(int id) => await _contexto.Pedidos.SingleAsync(x => x.Id == id);
    }
}