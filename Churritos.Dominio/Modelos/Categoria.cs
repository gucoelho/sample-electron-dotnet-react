using System.Collections.Generic;
using System.Linq;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;

namespace Churritos.Dominio.Modelos
{
    public class Categoria
    {
        public const string CategoriaCoberturasField = nameof(_categoriaCoberturas);
        public const string CategoriaRecheiosField = nameof(_categoriaRecheios);
        
        protected ICollection<CategoriaCobertura> _categoriaCoberturas;
        protected ICollection<CategoriaRecheio> _categoriaRecheios;
        
        public Categoria()
        {
            _categoriaCoberturas = new List<CategoriaCobertura>();
            _categoriaRecheios = new List<CategoriaRecheio>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public IReadOnlyCollection<Cobertura> Coberturas => _categoriaCoberturas.Select(x => x.Cobertura).ToArray();
        public IReadOnlyCollection<Recheio> Recheios => _categoriaRecheios.Select(x => x.Recheio).ToArray();                
    }
}