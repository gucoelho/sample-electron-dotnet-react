using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Churritos.Dominio.Modelos;
using Churritos.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Churritos.App.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdicionalController : ControllerBase
    {
        private AdicionalRepositório _repositorio;

        public AdicionalController(AdicionalRepositório repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IEnumerable<AdicionalViewModel>> Get()

        {
            var adicionais = await _repositorio.ObterTodosOsAdicionais();
            return adicionais.Select(x => new AdicionalViewModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Tipo = x.Tipo.ToString(),
                Valor = x.Valor
            });
        }

        public class AdicionalViewModel
        {
            public int Id { get; set; }
            public string Tipo { get; set; }
            public string Nome { get; set; }
            public decimal Valor { get; set; }
        }

    }
}