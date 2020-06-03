import React, { useState } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { formatarValor } from '../../utils'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import { Produto, Adicional } from './Models'
import ControleEtapas from './adicionar-item/ControleEtapas'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'

const Container = styled.div`
  display: flex;
`

const Etapas = styled.div`
  flex-grow: 1;
`

interface ItemPedido {
    produto: Produto,
    adicionais?: Adicional[]
}


// TODO: Ajustar fluxo de adição de novos item no pedido
const PaginaNovoPedido = ({ history }: any) => {
    const [itens, setItens] = useState<ItemPedido[]>([])
    const [adicionandoItem, setAdicionandoItem] = useState<boolean>(false)

    const adicionaItem = (item: ItemPedido) => {
        if (item)
            setItens([...itens, item])

        setAdicionandoItem(false)
    }

    const finalizarPedido = async () => {
        const rawResponse = await fetch('/api/pedido', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(itens.map(i => ({
                produtoId: i.produto.id,
                adicionais: i.adicionais
            })))
        })

        if (rawResponse.status === 200) {
            history.push('/pedidos')
        }
    }

    const calcularValorTotalProduto = (itemPedido: ItemPedido) => {
        let valorDosAdicionais = itemPedido.adicionais?.map(a => a.valor).reduce((a, acc) => acc + a, 0)

        if (!valorDosAdicionais)
            valorDosAdicionais = 0

        return itemPedido.produto.valor + valorDosAdicionais
    }


    //TODO: Lista de resumo com tabela
    return <Layout pagename="Novo Pedido">
        {!adicionandoItem &&
            <>
                <Button onClick={() => setAdicionandoItem(true)}>Adicionar Item</Button>
                <TableContainer component={Paper}>
                    <Table aria-label="collapsible table">
                        <TableHead>
                            <TableRow>
                                <TableCell />
                                <TableCell>Produto</TableCell>
                                <TableCell />
                                <TableCell />
                                <TableCell />
                                <TableCell align="right">Valor</TableCell>
                                <TableCell />
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {itens && itens.map(itemPedido => {
                                return (
                                    <>
                                        <TableRow key={itemPedido.produto.id}>
                                            <TableCell />
                                            <TableCell>
                                                {itemPedido.produto.nome}
                                            </TableCell>
                                            <TableCell />
                                            <TableCell />
                                            <TableCell />
                                            <TableCell align="right">+ {formatarValor(itemPedido.produto.valor)}</TableCell>
                                            <TableCell />
                                        </TableRow>
                                        {itemPedido.adicionais?.map(a =>
                                            <TableRow key={a.id}>
                                                <TableCell />
                                                <TableCell align="center">
                                                    <SubdirectoryArrowRightIcon />
                                                </TableCell>
                                                <TableCell>
                                                    {a.nome}
                                                </TableCell>
                                                <TableCell />
                                                <TableCell />
                                                <TableCell align="right">+ {formatarValor(a.valor)}</TableCell>
                                                <TableCell />
                                            </TableRow>
                                        )}

                                    </>
                                )
                            }
                            )}
                            <TableRow>
                                <TableCell />
                                <TableCell>
                                    <Typography variant="subtitle1">Total</Typography>
                                </TableCell>
                                <TableCell />
                                <TableCell />
                                <TableCell />
                                <TableCell align="right">
                                    <Typography variant="h6">
                                        {itens && formatarValor(itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0))}
                                    </Typography>
                                </TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </>}
        <Container>
            {adicionandoItem &&
                <Etapas>
                    <ControleEtapas adicionarItemPedido={adicionaItem} />
                </Etapas>
            }
            {itens.length > 0 && !adicionandoItem &&
                <Button onClick={finalizarPedido}>Finalizar Pedido</Button>
            }
        </Container>
    </Layout>
}


export default PaginaNovoPedido