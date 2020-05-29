using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositório _repositorio;
        private readonly RecheioRepositorio _recheioRepositorio;

        public ProdutoController(ProdutoRepositório repositorio, RecheioRepositorio recheioRepositorio)
        {
            _repositorio = repositorio;
            _recheioRepositorio = recheioRepositorio;
        }



        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> Get()
        {
            var produtos = await _repositorio.ObterTodosOsProdutos();

            var views = produtos.Select(x => new ProdutoViewModel
            {
                Id = x.Id,
                Valor = x.Valor,
                Nome = x.Nome,
                Categoria = x.Categoria.Nome
            });

            return views;
        }

        [HttpGet("{produtoid}/recheios")]
        public IEnumerable<Recheio> GetRecheios(int produtoId)
        {
            var produto = _repositorio.ObterProdutoPorId(produtoId);
            return produto.Recheios;
        }

        [HttpGet("{produtoid}/coberturas")]
        public IEnumerable<Cobertura> GetCoberturas(int produtoId)
        {
            var produto = _repositorio.ObterProdutoPorId(produtoId);
            return produto.Coberturas;
        }
    }
    
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public List<CoberturaViewModel> Coberturas { get;set; }
        public List<RecheioViewModel> Recheios { get;set; }
    }

    public class CoberturaViewModel
    {
        public int Id { get; set; }
        public int Nome { get; set; }
    }

    public class RecheioViewModel
    {
        public int Id { get; set; }
        public int Nome { get; set; }
    }
}