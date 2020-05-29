using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class ProdutoRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public ProdutoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;
        
        public async Task<IEnumerable<Produto>> ObterTodosOsProdutos() => await _contexto.Produtos.ToListAsync();
    }
}