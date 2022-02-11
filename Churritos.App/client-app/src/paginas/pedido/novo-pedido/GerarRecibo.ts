import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'
import { TDocumentDefinitions } from 'pdfmake/interfaces'
import { ItemPedido, Cliente, Endereco, PedidoDetalhe } from '../Models'
import moment from 'moment'
import { formatarValor } from '../../../utils'
pdfMake.vfs = pdfFonts.pdfMake.vfs



export const GerarReciboDoPedido = (pedido: PedidoDetalhe): void => {
    GerarRecibo(pedido.cliente, pedido.endereco, pedido.itens, pedido.origem, pedido.tipo, pedido.meioPagamento, pedido.taxaEntrega, pedido.desconto, pedido.dataCriacao)
}

export const GerarRecibo = (cliente: Cliente, endereco: Endereco, itens: ItemPedido[],
    origemSelecionada: string, tipoPedidoSelecionado: string, meioDePagamentoSelecionado: string,
    taxaEntrega: number, desconto: number, dataCriacao: Date): void => {

    const definitions = GerarDefiniçãoDeDocumento(cliente, endereco, itens, origemSelecionada, tipoPedidoSelecionado, meioDePagamentoSelecionado, taxaEntrega, desconto, dataCriacao)
    console.log(definitions)

    pdfMake.createPdf(definitions).download(`recibo-pedido-${moment(dataCriacao).format('YYYYMMDDHHmmss')}`)
}


const calcularValorTotalProduto = (itemPedido: ItemPedido): number => {
    if (itemPedido.produto) {
        let valorDosAdicionais = itemPedido.adicionais?.map(a => a.valor).reduce((a, acc) => acc + a, 0)

        if (!valorDosAdicionais)
            valorDosAdicionais = 0

        return itemPedido.produto.valor + valorDosAdicionais
    }
    return 0
}

const GerarDefiniçãoDeDocumento = (cliente: Cliente, endereco: Endereco, itens: ItemPedido[], origemSelecionada: string, tipoPedidoSelecionado: string, meioDePagamentoSelecionado: string,
    taxaEntrega: number, desconto: number, dataCriacao: Date): TDocumentDefinitions => {

    const valorTotalPedido: number = itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0) - (desconto ? desconto : 0) + taxaEntrega

    return ({
        pageSize: 'A6',
        pageMargins: [10, 10, 10, 10],
        defaultStyle: {
            fontSize: 8,
        },
        content: [
            {
                table: {
                    widths: ['*'],
                    body: [[
                        {
                            columns: [
                                {
                                    stack: [
                                        { text: origemSelecionada, alignment: 'center' },
                                        'Restaurante: Churritos',
                                        `Data ${moment(dataCriacao).format('DD/MM/YYYY HH:mm:ss')}`,
                                        ' ',
                                        'Dados do cliente',
                                        `Nome: ${cliente.nome}`,
                                        `CPF: ${cliente.cpf}`,
                                        `Telefone: ${cliente.telefone}`,
                                        `Endereço: ${endereco.logradouro}`,
                                        `Cidade: ${endereco.cidade}`,
                                        `Bairro: ${endereco.bairro}`,
                                        `Complemento: ${endereco.complemento}`,
                                        `Obs: ${endereco.observacao}`,
                                        ' ',
                                        'Itens do pedido:',
                                        {
                                            table: {
                                                widths: ['auto', '*', 'auto'],
                                                body: [
                                                    ['Categoria', 'Produto', 'Preço'],
                                                    ...itens.map(i => {
                                                        const header = [i.produto.categoria, i.produto.nome, formatarValor(i.produto.valor)]
                                                        const adicionais = i.adicionais?.map(a => [a.tipo, a.nome, formatarValor(a.valor)])
                                                        if (adicionais)
                                                            return [header, ...adicionais]
                                                        else
                                                            return [header]
                                                    }).flat()
                                                ]
                                            }
                                        },
                                        ' ',
                                        {
                                            table: {
                                                widths: ['*', 'auto'],
                                                body: [
                                                    ['Subtotal: ', { text: formatarValor(itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0)), alignment: 'right' }],
                                                    ['Taxa de entrega: ', { text: formatarValor(taxaEntrega), alignment: 'right' }],
                                                    ['Desconto: ', { text: '- ' + formatarValor(desconto), alignment: 'right' }],
                                                    ['Cobrar do cliente: ', { text: formatarValor(valorTotalPedido), alignment: 'right', bold: true }]
                                                ]
                                            }

                                        },
                                        ' ',
                                        `Forma de pagamento: ${meioDePagamentoSelecionado}`
                                    ]
                                }
                            ]
                        }
                    ]]
                }
            }
        ]
    })
}