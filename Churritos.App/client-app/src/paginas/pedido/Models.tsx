export interface Produto {
    id: number,
    nome: string,
    valor: number,
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