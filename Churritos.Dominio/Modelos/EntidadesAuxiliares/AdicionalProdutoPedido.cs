namespace Churritos.Dominio.Modelos.EntidadesAuxiliares
{
    public class AdicionalProdutoPedido
    {
        public int Id { get; set; }
        public int AdicionalId { get; set; }
        public int ProdutoPedidoId { get; set; }

        public Adicional Adicional { get; set; }
        public ProdutoPedido Produto { get; set; }
    }
}