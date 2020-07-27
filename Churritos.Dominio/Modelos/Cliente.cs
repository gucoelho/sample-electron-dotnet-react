using System.Collections.Generic;
using System.Runtime.Loader;

namespace Churritos.Dominio.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Telefone { get; set; }
        
        public IEnumerable<Endereço> Endereços { get; set; }
    }
}