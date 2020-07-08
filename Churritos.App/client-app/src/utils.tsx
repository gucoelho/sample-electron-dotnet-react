const formato = { minimumFractionDigits: 2 , style: 'currency', currency: 'BRL' }

export const formatarValor = (valor : number): string => valor.toLocaleString('pt-BR', formato)