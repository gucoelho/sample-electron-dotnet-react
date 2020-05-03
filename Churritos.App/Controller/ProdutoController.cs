using System.Collections.Generic;
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
        private readonly ItemRepositório _repositorio;

        public ProdutoController(ItemRepositório repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> Get() =>
            await _repositorio.ObterTodosOsItens();
    }
}