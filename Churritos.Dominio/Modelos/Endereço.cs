namespace Churritos.Dominio.Modelos
{
    public class Endereço
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Observação { get; set; }
        
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}