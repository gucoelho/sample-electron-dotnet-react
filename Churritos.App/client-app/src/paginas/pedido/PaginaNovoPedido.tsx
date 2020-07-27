import React, { useState, Fragment } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { formatarValor } from '../../utils'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import { Produto, Adicional } from './Models'
import ControleEtapas from './adicionar-item/ControleEtapas'
import Table from '@material-ui/core/Table'
import { TextField, InputAdornment, Chip, Grid, MenuItem } from '@material-ui/core'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import SubdirectoryArrowRightIcon from '@material-ui/icons/SubdirectoryArrowRight'
import NumberFormat from 'react-number-format'
import DeleteIcon from '@material-ui/icons/Delete'

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

const ContainerDados = styled(Paper)`
    padding: 15px;
    margin: 10px 0;
`

const CampoDesconto = styled(TextField)`
    & input {
        text-align: end;
    }
`
interface ActionBarProps { emProgresso: boolean }

const ActionBar = styled(Paper)`
    display: flex;
    align-items: center;
    justify-content: ${(props: ActionBarProps) => props.emProgresso ? 'space-between' : 'flex-end'};
    padding: 10px;
    margin-bottom: 10px;
`

const SecaoEdicao = styled.div`
  margin: 10px;
`

const origensDisponíveis = [
    'WhatsApp',
    'iFood'
]

const tipoPedido = [
    'App',
    'Padrão'
]

const meiosDePagamento = [
    'Cartão de crédito',
    'Dinheiro'
]

const PaginaNovoPedido = ({ history }: any) => {
    const [itens, setItens] = useState<ItemPedido[]>([])
    const [adicionandoItem, setAdicionandoItem] = useState<boolean>(false)
    const [desconto, setDesconto] = useState<number>(0.0)
    const [tipoPedidoSelecionado, setTipoPedidoSelecionado] = useState<string>(tipoPedido[0])
    const [origemSelecionada, setOrigemSelecionada] = useState<string>(origensDisponíveis[0])
    const [meioDePagamentoSelecionado, setMeioPagamentoSelecionado] = useState<string>(meiosDePagamento[0])
    const [taxaEntrega, setTaxaEntrega] = useState<number>(0.0)

    const adicionaItem = (item: ItemPedido) => {
        if (item)
            setItens([...itens, item])

        setAdicionandoItem(false)
    }

    const removerItem = (indice: number) => {
        itens.splice(indice, 1)
        setItens([...itens])
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
                    taxaEntrega,
                    desconto,
                    itens: itens.map(i => ({
                        produtoId: i.produto.id,
                        adicionais: i.adicionais
                    })),
                    origem: origemSelecionada,
                    tipo: tipoPedidoSelecionado,
                    meioDePagamento: meioDePagamentoSelecionado
                })
        })

        if (rawResponse.status === 200) {
            history.push('/pedidos')
        }
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

    const handleOrigemChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setOrigemSelecionada(event.target.value)
    const handleTipoPedidoChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setTipoPedidoSelecionado(event.target.value)
    const handleMeioPagamentosChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setMeioPagamentoSelecionado(event.target.value)
    const handleTaxaEntregaChange = (value: any) => value ? setTaxaEntrega(Number(value)) : setTaxaEntrega(0.0)
    const handleDescontoChange = (valor: any) => valor ? setDesconto(Number(valor)) : setDesconto(0.0)

    const valorTotalPedido: number = itens.map(x => calcularValorTotalProduto(x)).reduce((a, acc) => a + acc, 0) - (desconto ? desconto : 0) + taxaEntrega

    return <Layout pagename="Novo Pedido">
        {!adicionandoItem &&
            <>
                <ActionBar emProgresso={itens.length > 0}>
                    {itens.length > 0 && !adicionandoItem && valorTotalPedido > 0 &&
                        <Button onClick={finalizarPedido} color="primary" variant="contained">Finalizar Pedido</Button>
                    }
                    <Button variant="outlined" color="primary" onClick={(): void => setAdicionandoItem(true)}>Adicionar Item</Button>
                </ActionBar>


                <TableContainer component={Paper}>
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
                                                <Button color="primary" onClick={() => removerItem(i)}><DeleteIcon /></Button>
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

                <ContainerDados>
                    <Typography color="primary" variant="subtitle1" gutterBottom>Dados do pedido:</Typography>
                    <SecaoEdicao>
                        <Grid container spacing={3}>
                            <Grid item xs={3}>
                                <TextField
                                    select
                                    label="Origem"
                                    value={origemSelecionada ?? origensDisponíveis[0]}
                                    onChange={handleOrigemChange}
                                    variant="outlined"
                                    fullWidth
                                >
                                    {origensDisponíveis.map(o => <MenuItem key={o} value={o}>{o}</MenuItem>)}
                                </TextField>
                            </Grid>
                            <Grid item xs={3}>
                                <TextField
                                    select
                                    label="Tipo"
                                    value={tipoPedidoSelecionado ?? tipoPedido[0]}
                                    onChange={handleTipoPedidoChange}
                                    variant="outlined"
                                    fullWidth
                                >
                                    {tipoPedido.map(tp => <MenuItem key={tp} value={tp}>{tp}</MenuItem>)}
                                </TextField>
                            </Grid>
                            <Grid item xs={3}>
                                <TextField
                                    select
                                    label="Meio de pagamento"
                                    value={meioDePagamentoSelecionado ?? meiosDePagamento[0]}
                                    onChange={handleMeioPagamentosChange}
                                    variant="outlined"
                                    fullWidth
                                >
                                    {meiosDePagamento.map(mp => <MenuItem key={mp} value={mp}>{mp}</MenuItem>)}
                                </TextField>
                            </Grid>
                            <Grid item xs={3}>
                                <TextField
                                    label="Taxa de entrega"
                                    value={taxaEntrega}
                                    onChange={handleTaxaEntregaChange}
                                    InputProps={{
                                        startAdornment: <InputAdornment position="start">R$</InputAdornment>,
                                        inputComponent: NumberFormatCustom as any,
                                    }}
                                    variant="outlined"
                                    fullWidth
                                >
                                    {meiosDePagamento.map(mp => <MenuItem key={mp} value={mp}>{mp}</MenuItem>)}
                                </TextField>
                            </Grid>
                        </Grid>
                    </SecaoEdicao>
                </ContainerDados>


                <ContainerDados>
                    <Typography color="primary" variant="subtitle1" gutterBottom>Dados do cliente:</Typography>

                    <Grid container spacing={3}>
                        <Grid item xs={6}>
                            <TextField
                                label="Nome"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <TextField
                                label="CPF"
                                variant="outlined"
                                fullWidth
                            />
 
                        </Grid>

                        <Grid item xs={3}>
                            <TextField
                                label="Telefone/Celular"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>
                    </Grid>


                    <Typography color="primary" variant="subtitle1" gutterBottom>Endereço:</Typography>

                    <Grid container spacing={3}>
                        <Grid item xs={6}>
                            <TextField
                                label="Logradouro"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <TextField
                                label="Complemento"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <TextField
                                label="Bairro"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>

                        <Grid item xs={3}>
                            <TextField
                                label="Estado"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <TextField
                                label="Cidade"
                                variant="outlined"
                                fullWidth
                            />
                        </Grid>
                    </Grid>


                </ContainerDados>
            </>}
        <Container>
            {adicionandoItem &&
                <Etapas>
                    <ControleEtapas adicionarItemPedido={adicionaItem} />
                </Etapas>
            }
        </Container>
    </Layout>
}


export default PaginaNovoPedido