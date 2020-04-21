namespace Churritos.Dominio.Modelos
{
    public class Item
    {    
        public int Id { get; set; }

        public TipoItem Tipo { get; set; }
        public decimal Valor { get; set; } 
    }
}