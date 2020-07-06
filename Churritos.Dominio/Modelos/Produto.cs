using System.Collections.Generic;
using System.Linq;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;

namespace Churritos.Dominio.Modelos
{
    public class Produto
    {   

        public Produto()
        {
        }

        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public ICollection<AdicionalProduto> AdicionaisProduto { get; set; }
        public IReadOnlyCollection<Adicional> Adicionais => 
            AdicionaisProduto?.Select(x => x.Adicional).ToArray();                
    }
}