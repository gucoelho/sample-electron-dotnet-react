﻿using System.Collections;

 namespace Churritos.Dominio.Modelos
{
    public class ProdutoPedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public Cobertura CoberturaSelecionada { get; set; }
        public Recheio RecheioSelecionado { get; set; }
        
        public decimal Valor { get; set; }
    }
}