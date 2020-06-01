namespace Churritos.Dominio.Modelos.EntidadesAuxiliares
{
    public class AdicionalProduto
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        
        public int AdicionalId { get; set; }
        public Adicional Adicional  { get; set; }
    }
}