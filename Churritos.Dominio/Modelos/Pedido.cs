using System;
using System.Collections.Generic;
using System.Linq;

namespace Churritos.Dominio.Modelos
{
    public class Pedido
    {
        public static string ProdutosPedidoField = nameof(_produtos);
        protected ICollection<ProdutoPedido> _produtos;

        public Pedido() => _produtos = new List<ProdutoPedido>();
        
        public string Origem { get; set; }
        public string Tipo { get; set; }
        public int Id { get; set; }
        
        public DateTime DataCriação { get; set; }

        public IReadOnlyCollection<Produto> Produtos => _produtos.Select(x => x.Produto).ToArray();

        public Dictionary<Produto, Adicional[]> Adicionais =>
            _produtos
                .GroupBy(x => x.Produto)
                .ToDictionary(
                    x => x.Key, 
                    x => x.SelectMany(y => y.Adicionais).ToArray()
                    );

        public decimal ValorTotal => _produtos.Sum(x => x.ValorTotal) - Desconto;

        public decimal Desconto { get; set; } = 0.0m;
        
        public void AdicionarProdutoPedido(ProdutoPedido produtoPedido) => _produtos.Add(produtoPedido);
    }
}