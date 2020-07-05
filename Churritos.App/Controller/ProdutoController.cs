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
        
        [HttpGet("{id}")]
        public async Task<ProdutoDetalheViewModel> Get(int id)
        {
            var produto = await _repositorio.ObterProdutoPorId(id);
            var adicionais = await _adicionalRepositório.ObterTodosOsAdicionais();

            var relacao = adicionais.Select(adicional => new VinculoAdicionalViewModel()
            {
                Adicional = new AdicionalViewModel()
                {
                    Id = adicional.Id,
                    Nome = adicional.Nome,
                    Valor = adicional.Valor,
                    Tipo = adicional.Tipo.ToString()
                },
                Vinculado = produto.Adicionais.Contains(adicional)
            });

            return new ProdutoDetalheViewModel()
            {
                Id = produto.Id,
                CategoriaId = produto.Categoria.Id,
                Nome = produto.Nome,
                Valor = produto.Valor,
                
                AdicionaisVinculados = relacao
            };
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
        public async Task<IEnumerable<Adicional>> GetRecheios(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Recheio);
        }

        [HttpGet("{produtoid}/coberturas")]
        public async Task<IEnumerable<Adicional>> GetCoberturas(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Cobertura);
        }
        
        [HttpGet("{produtoid}/extras")]
        public async Task<IEnumerable<Adicional>> GetExtras(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Extra);
        }
    }

    public class VinculoAdicionalViewModel
    {
        public AdicionalViewModel Adicional { get; set; }
        public bool Vinculado { get; set; }
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
    
    
    public class ProdutoDetalheViewModel
    {
        public int Id { get; set; }
        public int CategoriaId  { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        
        public IEnumerable<VinculoAdicionalViewModel> AdicionaisVinculados { get; set; }
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