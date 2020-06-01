using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class AdicionalRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public AdicionalRepositório(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Adicional>> ObterTodosOsAdicionais() => await _contexto.Adicionais.ToListAsync();

        public IEnumerable<Adicional> ObterTodosOsAdicionaisDoProduto(int produtoId) => _contexto.Produtos
                .Include(x => x.AdicionaisProduto)
                    .ThenInclude(x => x.Adicional)
                    .ThenInclude(x => x.Tipo)
                .AsEnumerable()
                .Where(x => x.Id == produtoId)
                .SelectMany(x => x.Adicionais);

        public Adicional ObterCoberturaPorId(int coberturaId) => _contexto.Adicionais
            .Single(x => x.Id == coberturaId && x.Tipo == TipoAdicional.Cobertura);
        
         public Adicional ObterRecheioPorId(int recheioId) => _contexto.Adicionais
            .Single(x => x.Id == recheioId && x.Tipo == TipoAdicional.Recheio);
    }
}