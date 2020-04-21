﻿using System.Collections;

 namespace Churritos.Dominio.Modelos
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public Cobertura CoberturaSelecionada { get; set; }
        public Recheio RecheioSelecionado { get; set; }
    }
}