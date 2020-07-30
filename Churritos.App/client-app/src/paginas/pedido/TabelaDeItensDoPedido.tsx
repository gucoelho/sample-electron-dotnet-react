import React, { Fragment } from 'react'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { formatarValor } from '../../utils'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import { ItemPedido } from './Models'
import Table from '@material-ui/core/Table'
import { TextField, InputAdornment, Chip } from '@material-ui/core'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'
import NumberFormat from 'react-number-format'
import DeleteIcon from '@material-ui/icons/Delete'


const CelulaAdicional = styled(TableCell)`
    padding: 0.2rem 16px;
`

interface NumberFormatCustomProps {
    inputRef: (instance: NumberFormat | null) => void;
    onChange: (value: string) => void;
}

function NumberFormatCustom(props: NumberFormatCustomProps) {
    const { inputRef, onChange, ...other } = props

    return (
        <NumberFormat
            {...other}
            getInputRef={inputRef}
            onValueChange={(values) => onChange(values.value)}
            thousandSeparator={''}
            decimalSeparator=","
            isNumericString
            allowNegative={false}
            allowLeadingZeros={false}
            decimalScale={2}
            fixedDecimalScale
        />
    )
}

const CampoDesconto = styled(TextField)`
    & input {
        text-align: end;
    }
`

interface PropsTabelaDosItensDoPedido {
    somenteLeitura: boolean
    itens: ItemPedido[]
    removerItem?: (indice: number) => void
    desconto:number,
    handleDescontoChange?: (valor: any) => void
    valorTotalPedido: number
}
 

const TabelaDosItensDoPedido = ({
    itens,
    removerItem,
    desconto,
    handleDescontoChange,
    valorTotalPedido,
    somenteLeitura
} : PropsTabelaDosItensDoPedido) 
: JSX.Element => 
    (<TableContainer component={Paper}>
        <Table aria-label="collapsible table">
            <TableHead>
                <TableRow>
                    <TableCell />
                    <TableCell>Produto</TableCell>
                    <TableCell>Categoria</TableCell>
                    <TableCell />
                    <TableCell />
                    <TableCell align="right">Valor</TableCell>
                    <TableCell />
                </TableRow>
            </TableHead>
            <TableBody>
                {itens && itens.map((itemPedido, i) => {
                    return (
                        <Fragment key={itemPedido.produto.id} >
                            <TableRow>
                                <TableCell />
                                <TableCell>
                                    {itemPedido.produto.nome}
                                </TableCell>
                                <TableCell>
                                    <Chip label={itemPedido.produto.categoria} color="secondary" variant="outlined" />
                                </TableCell>
                                <TableCell />
                                <TableCell />
                                <TableCell align="right">+ {formatarValor(itemPedido.produto.valor)}</TableCell>
                                <TableCell>
                                    {!somenteLeitura &&
                                        <Button color="primary" onClick={() => removerItem ? removerItem(i) : null}><DeleteIcon /></Button>
                                    }
                                </TableCell>
                            </TableRow>
                            {itemPedido.adicionais?.map(a =>
                                <TableRow key={a.id}>
                                    <CelulaAdicional />
                                    <CelulaAdicional align="center">
                                        <SubdirectoryArrowRightIcon color="secondary" />
                                    </CelulaAdicional>
                                    <CelulaAdicional>
                                        <Chip label={a.tipo} color="primary" size="small" variant="outlined" />
                                    </CelulaAdicional>
                                    <CelulaAdicional>
                                        {a.nome}
                                    </CelulaAdicional>
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
                        <CampoDesconto
                            value={desconto}
                            onChange={handleDescontoChange}
                            InputProps={{
                                startAdornment: <InputAdornment position="start">- R$</InputAdornment>,
                                inputComponent: NumberFormatCustom as any,
                            }}
                            disabled={somenteLeitura}
                        />
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
    </TableContainer>)


export default TabelaDosItensDoPedido