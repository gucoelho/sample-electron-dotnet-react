import React, { useState } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { formatarValor } from '../../utils'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import Table from '@material-ui/core/Table'
import { TextField, InputAdornment } from '@material-ui/core'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'
import NumberFormat from 'react-number-format'
import { Produto, Adicional, ItemPedido } from './Models'

interface TabelaPedidoProps {
    itens: ItemPedido[],
    desconto: number
}

const CelulaAdicional = styled(TableCell)`
    padding: 0.2rem 16px;
`

const calcularValorTotalProduto = (itemPedido: ItemPedido): number => {
    let valorDosAdicionais = itemPedido.adicionais?.map(a => a.valor).reduce((a, acc) => acc + a, 0)

    if (!valorDosAdicionais)
        valorDosAdicionais = 0

    return itemPedido.produto.valor + valorDosAdicionais
}

const TabelaPedido = ({ itens, desconto }: TabelaPedidoProps) => {
    const valorTotalPedido: number = itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0) - (desconto ? desconto : 0)

    return <TableContainer component={Paper}>
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
                                    <CelulaAdicional />
                                    <CelulaAdicional align="center">
                                        <SubdirectoryArrowRightIcon />
                                    </CelulaAdicional>
                                    <CelulaAdicional>
                                        {a.nome}
                                    </CelulaAdicional>
                                    <CelulaAdicional />
                                    <CelulaAdicional />
                                    <CelulaAdicional align="right">+ {formatarValor(a.valor)}</CelulaAdicional>
                                    <CelulaAdicional />
                                </TableRow>
                            )}
                        </>
                    )
                }
                )}
                <TableRow>
                    <TableCell />
                    <TableCell>
                        <Typography variant="subtitle1">Desconto:</Typography>
                    </TableCell>
                    <TableCell />
                    <TableCell />
                    <TableCell />
                    <TableCell align="right">

                    </TableCell>
                    <TableCell />
                </TableRow>
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
                            {itens && formatarValor(valorTotalPedido)}
                        </Typography>
                    </TableCell>
                    <TableCell />
                </TableRow>
            </TableBody>
        </Table>
    </TableContainer>
}


export default TabelaPedido