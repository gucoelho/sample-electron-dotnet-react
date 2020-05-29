using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class ProdutoRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        private IQueryable<Produto> Consulta() => _contexto.Produtos
            .Include(x => x.ProdutoCoberturas)
                .ThenInclude(x => x.Cobertura)
            .Include(x => x.ProdutoRecheios)
                .ThenInclude(x => x.Recheio)
            .Include(x => x.Categoria);
        public ProdutoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Produto>> ObterTodosOsProdutos() => 
            await Consulta().ToListAsync();

        public Produto ObterProdutoPorId(int produtoId) => 
            Consulta().Single(x => x.Id == produtoId);
        
    }
}