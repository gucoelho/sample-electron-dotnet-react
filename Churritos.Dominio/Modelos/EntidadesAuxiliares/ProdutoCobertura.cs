namespace Churritos.Dominio.Modelos.EntidadesAuxiliares
{
    public class ProdutoCobertura
    {
        public int ProdutoId { get; set; }
        public int CoberturaId { get; set; }
        
        public Produto Produto { get; set; }
        public Cobertura Cobertura { get; set; }
    }
}