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
        private readonly AdicionalRepositório _adicionalRepositório;

        public ProdutoController(ProdutoRepositório repositorio, AdicionalRepositório adicionalRepositório)
        {
            _repositorio = repositorio;
            _adicionalRepositório = adicionalRepositório;
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
        
        
        [HttpGet("churros")]
        public async Task<IEnumerable<ProdutoViewModel>> GetTodosOsChurros()
        {
            var produtos = await _repositorio.ObterTodosOsChurros();

            var views = produtos.Select(x => new ProdutoViewModel
            {
                Id = x.Id,
                Valor = x.Valor,
                Nome = x.Nome,
                Categoria = x.Categoria.Nome
            });

            return views;
        }
        
        
        [HttpGet("bebidas")]
        public async Task<IEnumerable<ProdutoViewModel>> GetTodasAsBebidas()
        {
            var produtos = await _repositorio.ObterTodasAsBebidas();

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
        public IEnumerable<Adicional> GetRecheios(int produtoId)
        {
            var produto = _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Recheio);
        }

        [HttpGet("{produtoid}/coberturas")]
        public IEnumerable<Adicional> GetCoberturas(int produtoId)
        {
            var produto = _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Cobertura);
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