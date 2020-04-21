using System;
using System.Collections.Generic;
using System.Linq;

namespace Churritos.Dominio.Modelos
{
    public class Pedido
    {
        private readonly ICollection<ItemPedido> _itens;

        public Pedido()
        {
            _itens = new List<ItemPedido>();
        }
        
        public int Id { get; set; }
        
        public DateTime DataCriação { get; set; }

        public IEnumerable<Item> Itens => _itens.Select(x => x.Item);

        public decimal ValorTotal => Itens.Sum(x => x.Valor);

        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            _itens.Add(itemPedido);
        }
    }
}