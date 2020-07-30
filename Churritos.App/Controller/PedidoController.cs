using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepositório _repositorio;
        private readonly ProdutoRepositório _produtoRepositório;

        public PedidoController(PedidoRepositório repositorio,
            ProdutoRepositório produtoRepositório)
        {
            _repositorio = repositorio;
            _produtoRepositório = produtoRepositório;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoListaViewModel>> Get()
        {
            var pedidos = (await _repositorio.ObterTodosOsPedidos()).OrderByDescending(x => x.DataCriação);

            return pedidos.Select(x => new PedidoListaViewModel
            {
                Id = x.Id,
                Quantidade = x.Produtos.Count,
                DataCriacao = x.DataCriação,
                Valor = x.ValorTotal
            });
        }


        [HttpGet("{id}")]
        public async Task<PedidoDetalhadoViewModel> Get(int id)
        {
            var pedido = await _repositorio.ObterPedido(id);
            var clienteDoPedido = pedido.Cliente;
            var enderecoPrincipal = clienteDoPedido.Endereços.First();

            return new PedidoDetalhadoViewModel()
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriação,
                Valor = pedido.ValorTotal,
                Desconto = pedido.Desconto,
                Itens = pedido.Produtos.Select(p => new ItemPedidoViewModel()
                {
                    Adicionais = pedido.Adicionais[p].Select(a => new AdicionalViewModel()
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Valor = a.Valor,
                        Tipo = a.Tipo.ToString(),
                        TipoId = (int) a.Tipo
                    }).ToArray(),
                    ProdutoId = p.Id,
                    Produto = new ProdutoViewModel()
                    {
                       Id = p.Id,
                       Nome = p.Nome,
                       Valor = p.Valor,
                       Categoria = p.Categoria.Nome
                    }
                }),
                Cliente = new ClienteViewModel
                {
                    Cpf = clienteDoPedido.Cpf,
                    Nome = clienteDoPedido.Nome,
                    Telefone = clienteDoPedido.Telefone
                },
                Endereco = new EnderecoViewModel()
                {
                    Bairro = enderecoPrincipal.Bairro,
                    Cidade = enderecoPrincipal.Cidade,
                    Complemento = enderecoPrincipal.Complemento,
                    Logradouro = enderecoPrincipal.Logradouro,
                    Estado = enderecoPrincipal.Estado
                },
                Origem = pedido.Origem,
                Tipo = pedido.Tipo,
                MeioPagamento = pedido.MeioDePagamento,
                TaxaEntrega = pedido.TaxaDeEntrega
            };
        }

        [HttpGet("/api/pedidos")]
        public async Task<IEnumerable<PedidoListaViewModel>> GetByData([FromQuery] DateTime data)
        {
            var pedidos = (await _repositorio.ObterTodosOsPedidosDoDia(data)).OrderByDescending(x => x.DataCriação);
        
            return pedidos.Select(x => new PedidoListaViewModel
            {
                Id = x.Id,
                Quantidade = x.Produtos.Count,
                DataCriacao = x.DataCriação,
                Valor = x.ValorTotal,
                Origem = x.Origem,
                Tipo = x.Tipo,
                MeioPagamento = x.MeioDePagamento,
                TaxaEntrega = x.TaxaDeEntrega
            });
        }

        [HttpPost]
        public async Task Post(PedidoDTO pedidoDto)
        {
            var pedido = new Pedido()
            {
                Desconto = pedidoDto.Desconto,
                Origem = pedidoDto.Origem,
                Tipo = pedidoDto.Tipo,
                TaxaDeEntrega = pedidoDto.TaxaEntrega,
                MeioDePagamento = pedidoDto.MeioDePagamento,
                Cliente = new Cliente()
                {
                    Cpf = pedidoDto.Cliente.Cpf,
                    Nome = pedidoDto.Cliente.Nome,
                    Telefone = pedidoDto.Cliente.Telefone,
                    Endereços = new List<Endereço>()
                    {
                        new Endereço
                        {
                            Logradouro = pedidoDto.Endereco.Logradouro,
                            Bairro = pedidoDto.Endereco.Bairro,
                            Cidade = pedidoDto.Endereco.Cidade,
                            Estado = pedidoDto.Endereco.Estado,
                            Complemento = pedidoDto.Endereco.Complemento 
                        }
                    }
                }
            };

            foreach (var item in pedidoDto.Itens)
            {
                var produto = await _produtoRepositório.ObterProdutoPorId(item.ProdutoId);
                var produtoPedido = new ProdutoPedido
                {
                    Produto = produto,
                    Valor = produto.Valor
                };

                if (item.Adicionais?.Length > 0)
                {
                    var vinculoAdicionais = item.Adicionais.Select(x =>
                    {
                        var adicional = produto.Adicionais.Single(y => y.Id == x.Id);
                        return new AdicionalProdutoPedido
                        {
                            Produto = produtoPedido,
                            Adicional = adicional,
                            Valor = adicional.Valor 
                        };
                    }).ToList();

                    produtoPedido.AdicionaisProdutoPedido = vinculoAdicionais;
                }

                pedido.AdicionarProdutoPedido(produtoPedido);
            }

            await _repositorio.AdicionarPedido(pedido);
        }

        [HttpGet("download/{data}")]
        public async Task<IEnumerable<PedidoDownloadViewModel>> GetPedidosDownload(DateTime data)
        {
            var pedidos = await _repositorio.ObterTodosOsPedidosDoDia(data);

            var download = pedidos
                .SelectMany(pedido =>
                {
                    var pedidoDownload = pedido.Produtos
                        .SelectMany(produto =>
                        {
                            var listaDeRetorno = new List<PedidoDownloadViewModel>
                            {
                                new PedidoDownloadViewModel
                                {
                                    PedidoId = pedido.Id,
                                    Data = pedido.DataCriação,
                                    ProdutoId = produto.Id,
                                    NomeProduto = produto.Nome,
                                    Valor = produto.Valor
                                }
                            };

                            if (pedido.Adicionais[produto].Any())
                                listaDeRetorno.AddRange(pedido.Adicionais[produto]
                                    .Select(adicional =>
                                        new PedidoDownloadViewModel
                                        {
                                            PedidoId = pedido.Id,
                                            Data = pedido.DataCriação,
                                            ProdutoId = produto.Id,
                                            NomeProduto = produto.Nome,
                                            Valor = adicional.Valor,
                                            AdicionalId = adicional.Id,
                                            AdicionalNome = adicional.Nome
                                        }));

                            return listaDeRetorno;
                        }).ToList();
                        
                    if(pedido.Desconto > 0)
                        pedidoDownload.Add(new PedidoDownloadViewModel
                        {
                            Data = pedido.DataCriação,
                            Valor = - pedido.Desconto,
                            PedidoId = pedido.Id,
                            NomeProduto = "Desconto"
                        });

                    return pedidoDownload;
                });

            return download.ToList();
        }
    }

    public class PedidoDownloadViewModel
    {
        public int PedidoId { get; set; }
 
        public string Local { get; set; }
        
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int AdicionalId { get; set; }
        public string AdicionalNome { get; set; }
        public decimal Valor { get; set; }
    }

    public class PedidoListaViewModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        
        public string Origem { get; set; }
        
        public string Tipo { get; set; }
        
        public string MeioPagamento { get; set; }
        
        public decimal TaxaEntrega { get; set; }
    }

    public class ItemPedidoViewModel
    {
        public int ProdutoId { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public AdicionalViewModel[] Adicionais { get; set; }
    }

    public class PedidoDTO
    {
        public IEnumerable<ItemPedidoViewModel> Itens { get; set; }
        public decimal Desconto { get; set; }
        
        public string Origem { get; set; }
        
        public string Tipo { get; set; }
        
        public string MeioDePagamento { get; set; }
        
        public decimal TaxaEntrega { get; set; }
        
        public int TempoEstimado { get; set; }
        
        public ClienteDTO Cliente { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }

    public class EnderecoDTO
    {
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
    }

    public class ClienteDTO
    { 
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
    }

    public class PedidoDetalhadoViewModel
    {
        public IEnumerable<ItemPedidoViewModel> Itens { get; set; }
        public decimal Desconto { get; set; }
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal Valor { get; set; }

        public ClienteViewModel Cliente { get; set; }
        
        public EnderecoViewModel Endereco { get; set; }
        public string Origem { get; set; }
        public string Tipo { get; set; }
        public string MeioPagamento { get; set; }
        public decimal TaxaEntrega { get; set; }
    }

    public class EnderecoViewModel
    {
         public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
    }

    public class ClienteViewModel
    {
        
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
    }

    public class AdicionalViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}