namespace Churritos.Dominio.Modelos
{
    public class Adicional
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        
        public TipoAdicional Tipo { get; set; }
    }

    public enum TipoAdicional
    {
        Recheio,
        Cobertura,
        Extra
    }
}