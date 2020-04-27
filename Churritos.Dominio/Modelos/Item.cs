namespace Churritos.Dominio.Modelos
{
    public class Item
    {    
        public int Id { get; set; }

        public Categoria Categoria { get; set; }
        public decimal Valor { get; set; } 
    }
}