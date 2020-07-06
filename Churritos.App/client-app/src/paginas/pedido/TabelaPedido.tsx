import React, { Fragment } from 'react'
import Typography from '@material-ui/core/Typography'
import { formatarValor } from '../../utils'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'
import { PedidoDetalhe } from './Models'

interface TabelaPedidoProps {
    pedido: PedidoDetalhe
}

const CelulaAdicional = styled(TableCell)`
    padding: 0.2rem 16px;
`

const TabelaPedido = ({ pedido }: TabelaPedidoProps) => {
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
                {pedido.itens && pedido.itens.map(itemPedido => {
                    return (
                        <Fragment key={itemPedido.produto.id}>
                            <TableRow>
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
                        </Fragment>
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
                            {formatarValor(pedido.valor)}
                        </Typography>
                    </TableCell>
                    <TableCell />
                </TableRow>
            </TableBody>
        </Table>
    </TableContainer>
}


export default TabelaPedido