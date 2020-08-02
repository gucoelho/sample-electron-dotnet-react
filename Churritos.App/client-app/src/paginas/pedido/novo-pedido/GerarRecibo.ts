import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'
import { TDocumentDefinitions } from 'pdfmake/interfaces'
import { ItemPedido, Cliente, Endereco } from '../Models' 
import moment from 'moment'
pdfMake.vfs = pdfFonts.pdfMake.vfs

export const GerarRecibo = (cliente: Cliente, endereco: Endereco, itens: ItemPedido[]) : void => { 
    const definitions = GerarDefiniçãoDeDocumento(cliente, endereco, itens)
    console.log(definitions) 

    const pdfDocGenerator = pdfMake.createPdf(definitions)
    pdfDocGenerator.getDataUrl((dataUrl) => {
        const iframe = document.getElementById('recibo-pdf') as HTMLIFrameElement
        iframe.src = dataUrl
    })

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

const GerarDefiniçãoDeDocumento = (cliente: Cliente, endereco: Endereco, itens: ItemPedido[]) : TDocumentDefinitions => ({
    pageSize: 'A6',
    pageMargins: [ 10,10,10,10 ],
    defaultStyle: {
        fontSize: 8,
    },
    content: [
        { table: { 
            widths: ['*'],
            body: [[
                { columns: [
                    { 
                        stack: [
                            { text : 'WhatsApp', alignment: 'center'},
                            'Restaurante: Churritos',
                            `Data ${moment().format('DD/MM/YYYY HH:mm:ss')}`,
                            ' ',
                            'Dados do cliente',
                            `Nome: ${cliente.nome}`,
                            `CPF: ${cliente.cpf}`,
                            `Telefone: ${cliente.telefone}`,
                            `Endereço: ${endereco.logradouro}`,
                            `Cidade: ${endereco.cidade}`,
                            `Bairro: ${endereco.bairro}`,
                            `Complemento: ${endereco.complemento}`,
                            'Obs: Teste',
                            ' ',
                            'Itens do pedido:',
                            {
                                table: {
                                    widths: ['auto', '*', 'auto'],
                                    body: [
                                        ['Categoria', 'Produto', 'Preço'],
                                        ...itens.map(i => { 
                                            const header = [i.produto.categoria, i.produto.nome, i.produto.valor]
                                            const adicionais = i.adicionais?.map(a => [a.tipo, a.nome, a.valor])

                                            return [header, ...adicionais]
                                        }).flat()
                                    ]  
                                }
                            },
                            ' ',
                            {
                                table: {
                                    widths: ['*', 'auto'],
                                    body: [
                                        ['Subtotal: ', 'R$ ' + itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0)],
                                        ['Taxa de entrega: ', 'R$'],
                                        ['Desconto: ', 'R$'],
                                        ['Cobrar do cliente: ', 'R$']
                                    ]  
                                }
    
                            },
                            ' ',
                            'Forma de pagamento: Cartao de Credito'
                        ]}
                ]}
            ]]}}
    ]
}) 