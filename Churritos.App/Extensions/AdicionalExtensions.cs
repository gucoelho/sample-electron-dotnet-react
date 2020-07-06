using Churritos.App.Controller;
using Churritos.Dominio.Modelos;

namespace Churritos.App.Extensions
{
    public static class AdicionalExtensions
    {
        public static AdicionalViewModel ToViewModel(this Adicional adicional) => new AdicionalViewModel
        {
            Id = adicional.Id,
            Nome = adicional.Nome,
            Tipo = adicional.Tipo.ToString(),
            TipoId = (int) adicional.Tipo,
            Valor = adicional.Valor
        };
    }
}