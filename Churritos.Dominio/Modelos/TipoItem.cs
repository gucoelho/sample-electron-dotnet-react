using System.Collections.Generic;
using System.Linq;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;

namespace Churritos.Dominio.Modelos
{
    public class TipoItem
    {
        public const string TipoItemCoberturasField = nameof(_tipoItemCoberturas);
        public const string TipoItemRecheiosField = nameof(_tipoItemRecheios);
        
        ICollection<TipoItemCobertura> _tipoItemCoberturas;
        ICollection<TipoItemRecheio> _tipoItemRecheios;
        
        public TipoItem()
        {
            _tipoItemCoberturas = new List<TipoItemCobertura>();
            _tipoItemRecheios = new List<TipoItemRecheio>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public IReadOnlyCollection<Cobertura> Coberturas => _tipoItemCoberturas.Select(x => x.Cobertura).ToArray();
        public IReadOnlyCollection<Recheio> Recheio => _tipoItemRecheios.Select(x => x.Recheio).ToArray();                
    }
}