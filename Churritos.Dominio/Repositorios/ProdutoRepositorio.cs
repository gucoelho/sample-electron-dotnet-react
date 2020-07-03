using System.Collections;
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
            .Include(x => x.AdicionaisProduto)
                .ThenInclude(x => x.Adicional)
            .Include(x => x.Categoria);
        public ProdutoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Produto>> ObterTodosOsProdutos() => 
            await Consulta().ToListAsync();

        public async Task<IEnumerable<Produto>> ObterTodosOsChurros() =>
            await Consulta().Where(x => x.Categoria.Nome == "Churros").ToListAsync();

        public async Task<Produto> ObterProdutoPorId(int produtoId) => 
            await Consulta().SingleAsync(x => x.Id == produtoId);

        public async Task<IEnumerable<Produto>> ObterTodasAsBebidas() => 
            await Consulta().Where(x => x.Categoria.Nome == "Bebidas").ToListAsync();

    }
}