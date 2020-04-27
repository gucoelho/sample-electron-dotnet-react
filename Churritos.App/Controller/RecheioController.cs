using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecheioController : ControllerBase
    {
        private RecheioRepositorio _repositorio;

        public RecheioController(RecheioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Recheio>> Get() => 
            await _repositorio.ObterTodosOsRecheios();
    }
}