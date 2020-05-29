using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class RecheioRepositorio
    {
        private readonly ContextoDaAplicação _contexto;

        public RecheioRepositorio(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Recheio>> ObterTodosOsRecheios() => await _contexto.Recheios.ToListAsync();

        public IEnumerable<Recheio> ObterTodosOsRecheiosDaCategoria(int categoriaId) => _contexto.Categorias
                .Include($"{Categoria.CategoriaRecheiosField}")
                .AsEnumerable()
                .Where(x => x.Id == categoriaId)
                .SelectMany(x => x.Recheios);
    }
}