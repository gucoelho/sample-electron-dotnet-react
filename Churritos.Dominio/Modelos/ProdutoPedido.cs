 using System;
 using System.Collections.Generic;
 using System.Linq;
 using Churritos.Dominio.Modelos.EntidadesAuxiliares;

 namespace Churritos.Dominio.Modelos
{
    public class ProdutoPedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        
        public IEnumerable<AdicionalProdutoPedido> AdicionaisProdutoPedido { get; set; }
        public IEnumerable<Adicional> Adicionais => AdicionaisProdutoPedido.Select(x => x.Adicional);
        public decimal Valor { get; set; }
        public decimal ValorTotal => Adicionais.Sum(a => a.Valor) + Valor;
    }
}