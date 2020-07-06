export interface Produto {
    id: number,
    nome: string,
    valor: number,
    categoria: Categoria
}

export interface Categoria {
    id: number,
    nome: string
}

export interface Adicional {
    id: number,
    nome: string,
    valor: number,
    tipo: string,
    tipoId: number,
}

export interface ItemPedido {
    produto: Produto,
    adicionais?: Adicional[]
}

export interface PedidoDetalhe {
    id: number,
    dataCriacao: Date,
    valor: number,
    desconto: number
    itens: ItemPedido[]
}