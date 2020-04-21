using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class CoberturaRepositorio
    {
        private readonly ContextoDaAplicação _contexto;

        public CoberturaRepositorio(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Cobertura>> ObterTodosAsCoberturas() => await _contexto.Coberturas.ToListAsync();
    }
}