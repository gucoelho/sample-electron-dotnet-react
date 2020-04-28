using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class CategoriaRepositorio
    {
        private readonly ContextoDaAplicação _contexto;

        public CategoriaRepositorio(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Categoria>> ObterTodasAsCategorias() => 
            await _contexto.Set<Categoria>()
                .Include($"{Categoria.CategoriaRecheiosField}")
                .Include($"{Categoria.CategoriaCoberturasField}")
                .ToListAsync();
    }
}