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
import {TextField, InputAdornment} from '@material-ui/core'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'
import NumberFormat from 'react-number-format';

const Container = styled.div`
  display: flex;
`

const Etapas = styled.div`
  flex-grow: 1;
`

interface ItemPedido {
    produto: Produto;
    adicionais?: Adicional[];
}

const CelulaAdicional = styled(TableCell)`
    padding: 0.2rem 16px;
`

interface NumberFormatCustomProps {
  inputRef: (instance: NumberFormat | null) => void;
  onChange: (value: string) => void;
}

function NumberFormatCustom(props: NumberFormatCustomProps) {
  const { inputRef, onChange, ...other } = props;

  return (
    <NumberFormat
      {...other}
      getInputRef={inputRef}
      onValueChange={(values) => onChange(values.value)}
      thousandSeparator="."
      decimalSeparator=","
      isNumericString
      allowNegative={false}
    />
  );
}

const CampoDesconto = styled(TextField)`
    & input {
        text-align: end;
    }
`

// TODO: Ajustar fluxo de adição de novos item no pedido
const PaginaNovoPedido = ({ history }: any) => {
    const [itens, setItens] = useState<ItemPedido[]>([])
    const [adicionandoItem, setAdicionandoItem] = useState<boolean>(false)
    const [desconto, setDesconto] = useState<number>()

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
            body: JSON.stringify(
                { 
                    desconto: desconto,  
                    itens : itens.map(i => ({
                        produtoId: i.produto.id,
                        adicionais: i.adicionais
                    }))
                })
            })

        if (rawResponse.status === 200) {
            history.push('/pedidos')
        }
    }

    const calcularValorTotalProduto = (itemPedido: ItemPedido): number => {
        let valorDosAdicionais = itemPedido.adicionais?.map(a => a.valor).reduce((a, acc) => acc + a, 0)

        if (!valorDosAdicionais)
            valorDosAdicionais = 0

        return itemPedido.produto.valor + valorDosAdicionais
    }

    const handleChange = (valor : any) => {
        if(valor)
            setDesconto(Number(valor))
        else 
            setDesconto(valor)
    };

    const valorTotalPedido: number = itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0) - (desconto ? desconto : 0)

    
    return <Layout pagename="Novo Pedido">
        {!adicionandoItem &&
            <>
                <Button onClick={(): void => setAdicionandoItem(true)}>Adicionar Item</Button>
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
                                        <CampoDesconto
                                            value={desconto}
                                            onChange={handleChange}
                                            InputProps={{
                                                startAdornment: <InputAdornment position="start">- R$</InputAdornment>,
                                                inputComponent: NumberFormatCustom as any,
                                            }}
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
                </TableContainer>
            </>}
        <Container>
            {adicionandoItem &&
                <Etapas>
                    <ControleEtapas adicionarItemPedido={adicionaItem} />
                </Etapas>
            }
            {itens.length > 0 && !adicionandoItem && valorTotalPedido > 0 &&
                <Button onClick={finalizarPedido}>Finalizar Pedido</Button>
            }
        </Container>
    </Layout>
}


export default PaginaNovoPedido