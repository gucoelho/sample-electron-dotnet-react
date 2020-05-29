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

        public IReadOnlyCollection<ProdutoCobertura> ProdutoCoberturas { get; set; }
        public IReadOnlyCollection<ProdutoRecheio> ProdutoRecheios { get; set; }
        public IReadOnlyCollection<Cobertura> Coberturas => ProdutoCoberturas.Select(x => x.Cobertura).ToArray();
        public IReadOnlyCollection<Recheio> Recheios => ProdutoRecheios.Select(x => x.Recheio).ToArray();                
    }
}