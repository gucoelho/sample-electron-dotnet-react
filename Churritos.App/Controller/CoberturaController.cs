using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoberturaController : ControllerBase
    {
        private CoberturaRepositorio _repositorio;

        public CoberturaController(CoberturaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Cobertura>> Get() => 
            await _repositorio.ObterTodosAsCoberturas();
    }
}