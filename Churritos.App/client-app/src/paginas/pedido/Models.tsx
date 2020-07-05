export interface Produto {
    id: number,
    nome: string,
    valor: number,
    categoriaId?: number
}

export interface Adicional {
    id: number,
    nome: string,
    valor: number,
    tipo: string
}

export interface ItemPedido {
    produto: Produto,
    adicionais?: Adicional[]
}