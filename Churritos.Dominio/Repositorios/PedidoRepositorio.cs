﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Modelos.EntidadesAuxiliares;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Repositorios
{
    public class PedidoRepositório
    {
        private readonly ContextoDaAplicação _contexto;

        public PedidoRepositório(ContextoDaAplicação contexto) => _contexto = contexto;

        protected IQueryable<Pedido> Consulta() =>
            _contexto.Pedidos
                .Include($"{Pedido.ProdutosPedidoField}.{nameof(ProdutoPedido.Produto)}")
                .Include(
                    $"{Pedido.ProdutosPedidoField}.{nameof(ProdutoPedido.AdicionaisProdutoPedido)}.{nameof(AdicionalProdutoPedido.Adicional)}");

        public async Task<IEnumerable<Pedido>> ObterTodosOsPedidos() => await Consulta().ToListAsync();

        public async Task AdicionarPedido(Pedido pedido)
        {
            pedido.DataCriação = DateTime.Now;
            _contexto.Add(pedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task ObterPedido(int id) => await _contexto.Pedidos.SingleAsync(x => x.Id == id);

        public async Task<IEnumerable<Pedido>> ObterTodosOsPedidosDoDia(DateTime data) =>
            await Consulta().Where(x => x.DataCriação.Date == data.Date).ToListAsync();

    }
}