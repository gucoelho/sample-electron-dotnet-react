using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class ItemRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public ItemRepositório(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Item>> ObterTodosOsItens() => await _contexto.Itens.ToListAsync();
    }
}