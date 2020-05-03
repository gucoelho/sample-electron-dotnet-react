namespace Churritos.Dominio.Modelos
{
    public class Item
    {    
        public int Id { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; } 
    }
}