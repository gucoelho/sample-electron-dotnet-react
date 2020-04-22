using System;
using System.Collections.Generic;
using System.Linq;

namespace Churritos.Dominio.Modelos
{
    public class Pedido
    {
        public static string ItensPedidoField = nameof(_itens);
        protected ICollection<ItemPedido> _itens;

        public Pedido() => _itens = new List<ItemPedido>();

        public int Id { get; set; }
        
        public DateTime DataCriação { get; set; }

        public IReadOnlyCollection<Item> Itens => _itens.Select(x => x.Item).ToArray();

        public decimal ValorTotal => Itens.Sum(x => x.Valor);

        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            _itens.Add(itemPedido);
        }
    }
}