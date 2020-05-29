namespace Churritos.Dominio.Modelos.EntidadesAuxiliares
{
    public class ProdutoRecheio
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        
        public int RecheioId { get; set; }
        public Recheio Recheio  { get; set; }
    }
}