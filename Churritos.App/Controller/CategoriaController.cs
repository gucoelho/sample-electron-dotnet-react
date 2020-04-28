using System.Collections.Generic;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _repositorio;
        
        public CategoriaController(CategoriaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Categoria>> Get() => 
            await _repositorio.ObterTodasAsCategorias();
    }
}