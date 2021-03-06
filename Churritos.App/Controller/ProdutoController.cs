﻿using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Churritos.App.Extensions;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
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
                Categoria = produto.Categoria,
                Nome = produto.Nome,
                Valor = produto.Valor,
                
                AdicionaisVinculados = relacao
            };
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarProdutoCommand criarProduto)
        {
            var produto = new Produto();

            produto.Nome = criarProduto.Nome;
            produto.CategoriaId = criarProduto.CategoriaSelecionada;
            produto.Valor = criarProduto.Valor;

            produto.AdicionaisProduto = criarProduto.AdicionaisVinculados
                .Where(x => x.Vinculado)
                .Select(x => new AdicionalProduto() {AdicionalId = x.Adicional.Id, ProdutoId = produto.Id}).ToList();

            await _repositorio.AdicionarProduto(produto);

            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarProdutoCommand editarProduto)
        {
            var produto = await _repositorio.ObterProdutoPorId(id);

            produto.Nome = editarProduto.Nome;
            produto.CategoriaId = editarProduto.CategoriaSelecionada;
            produto.Valor = editarProduto.Valor;

            produto.AdicionaisProduto = editarProduto.AdicionaisVinculados
                .Where(x => x.Vinculado)
                .Select(x => new AdicionalProduto() {AdicionalId = x.Adicional.Id, ProdutoId = produto.Id}).ToList();

            await _repositorio.AtualizarProduto(produto);

            return Ok();
        }

        public class CriarProdutoCommand
        {
            public int CategoriaSelecionada { get; set; }
            public string Nome { get; set; }
            public decimal Valor { get; set; }
            public IEnumerable<VinculoAdicionalViewModel> AdicionaisVinculados { get; set; }
        }
        public class EditarProdutoCommand
        {
            public int Id { get; set; }
            public int CategoriaSelecionada { get; set; }
            public string Nome { get; set; }
            public decimal Valor { get; set; }
            public IEnumerable<VinculoAdicionalViewModel> AdicionaisVinculados { get; set; }
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
        public async Task<IEnumerable<AdicionalViewModel>> GetRecheios(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Recheio).Select(x => x.ToViewModel());
        }

        [HttpGet("{produtoid}/coberturas")]
        public async Task<IEnumerable<AdicionalViewModel>> GetCoberturas(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Cobertura).Select(x => x.ToViewModel());;
        }
        
        [HttpGet("{produtoid}/extras")]
        public async Task<IEnumerable<AdicionalViewModel>> GetExtras(int produtoId)
        {
            var produto = await _repositorio.ObterProdutoPorId(produtoId);
            return produto.Adicionais.Where(x => x.Tipo == TipoAdicional.Extra).Select(x => x.ToViewModel());;
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
        public Categoria Categoria  { get; set; }
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