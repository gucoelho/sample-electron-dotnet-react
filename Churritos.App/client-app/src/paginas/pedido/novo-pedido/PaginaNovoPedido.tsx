import React, { useState, useContext, ChangeEvent } from 'react'
import Layout from '../../Layout'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import { Paper } from '@material-ui/core'
import styled from 'styled-components'
import { Produto, Adicional } from '../Models'
import ControleEtapas from '../adicionar-item/ControleEtapas'
import { TextField, InputAdornment, Grid, MenuItem } from '@material-ui/core'
import NumberFormat from 'react-number-format'
import TabelaDosItensDoPedido from '../TabelaDeItensDoPedido'
import { NovoPedidoContext } from './NovoPedidoContext'

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
    const { cliente, endereco } = useContext(NovoPedidoContext)

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
                    cliente,
                    endereco,
                    taxaEntrega,
                    desconto,
                    origem: origemSelecionada,
                    tipo: tipoPedidoSelecionado,
                    meioDePagamento: meioDePagamentoSelecionado,
                    itens: itens.map(i => ({
                        produtoId: i.produto.id,
                        adicionais: i.adicionais
                    })),
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

                <TabelaDosItensDoPedido 
                    desconto={desconto}
                    handleDescontoChange={handleDescontoChange}
                    itens={itens}
                    removerItem={removerItem}
                    valorTotalPedido={valorTotalPedido}
                    somenteLeitura={false}
                />

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
                <ClienteForm />
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

interface PropsClientForm {
    somenteLeitura? : boolean
}

const ClienteForm = ({somenteLeitura} : PropsClientForm = { somenteLeitura: false }) => {
    const { cliente, setCliente, endereco, setEndereco } = useContext(NovoPedidoContext)

    const handleCampoCliente = (event : ChangeEvent<HTMLInputElement>) : void => {
        setCliente({...cliente, [event.target.name]: event.target.value })
    }

    const handleCampoEndereco = (event : ChangeEvent<HTMLInputElement>) : void => {
        setEndereco({...endereco, [event.target.name]: event.target.value })
    }

    return (
        <>
            <ContainerDados>
                <Typography color="primary" variant="subtitle1" gutterBottom>Dados do cliente:</Typography>
                <Grid container spacing={3}>
                    <Grid item xs={6}>
                        <TextField
                            label="Nome"
                            variant="outlined"
                            fullWidth
                            name="nome"
                            value={cliente.nome}
                            onChange={handleCampoCliente}
                            disabled={somenteLeitura}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="CPF"
                            variant="outlined"
                            fullWidth
                            name="cpf"
                            value={cliente.cpf}
                            onChange={handleCampoCliente}
                        />
 
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Telefone/Celular"
                            variant="outlined"
                            fullWidth
                            name="telefone"
                            value={cliente.telefone}
                            onChange={handleCampoCliente}
                            disabled={somenteLeitura}
                        />
                    </Grid>
                </Grid>
            </ContainerDados>
            <ContainerDados>
                <Typography color="primary" variant="subtitle1" gutterBottom>Endereço:</Typography>

                <Grid container spacing={3}>
                    <Grid item xs={6}>
                        <TextField
                            label="Logradouro"
                            variant="outlined"
                            fullWidth
                            name="logradouro"
                            value={endereco.logradouro}
                            onChange={handleCampoEndereco}
                            disabled={somenteLeitura}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Complemento"
                            variant="outlined"
                            fullWidth
                            name="complemento"
                            value={endereco.complemento}
                            onChange={handleCampoEndereco}
                            disabled={somenteLeitura}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Bairro"
                            variant="outlined"
                            fullWidth
                            name="bairro"
                            value={endereco.bairro}
                            onChange={handleCampoEndereco}
                            disabled={somenteLeitura}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Estado"
                            variant="outlined"
                            fullWidth
                            name="estado"
                            value={endereco.estado}
                            disabled
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <TextField
                            label="Cidade"
                            variant="outlined"
                            fullWidth
                            name="cidade"
                            value={endereco.cidade}
                            onChange={handleCampoEndereco}
                            disabled={somenteLeitura}
                        />
                    </Grid>
                </Grid>
            </ContainerDados>
        </>
    )
}

export default PaginaNovoPedido