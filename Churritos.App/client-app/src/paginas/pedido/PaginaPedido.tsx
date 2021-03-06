import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import { PedidoDetalhe, Cliente, Endereco } from './Models'
import { Button, LinearProgress, Typography, Grid, TextField, Paper } from '@material-ui/core'
import TabelaDosItensDoPedido from './TabelaDeItensDoPedido'
import styled from 'styled-components'
import { formatarValor } from '../../utils'
import { GerarReciboDoPedido } from './novo-pedido/GerarRecibo'

const ContainerDados = styled(Paper)`
    padding: 15px;
    margin: 10px 0;
`

const ActionBar = styled(Paper)`
    display: flex;
    align-items: center;
    justify-content: 'flex-end';
    padding: 10px;
    margin-bottom: 10px;
`
interface Params {
    id: number
}

interface Match {
    params: Params,
}

interface PaginaPedidoProps {
    match: Match
}

const PaginaPedidos = ({ match: { params } }: PaginaPedidoProps) => {
    const [pedido, setPedido] = useState<PedidoDetalhe>()
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)

        fetch(`/api/pedido/${params.id}`)
            .then(res => res.json())
            .then(data => setPedido(data))
            .then(() => setLoading(false))

    }, [params.id])

    return <Layout pagename={`Pedido ${params.id}`} >
        {loading && <LinearProgress />}
        {!loading && pedido && 
        <>
            <ActionBar>
                <Button color="primary" variant="contained" onClick={() => GerarReciboDoPedido(pedido)}>
                Baixar recibo
                </Button>
            </ActionBar>
            <TabelaDosItensDoPedido 
                itens={pedido.itens}
                desconto={pedido.desconto}
                valorTotalPedido={pedido.valor}
                somenteLeitura
            />
            <AreaPedido 
                origem={pedido.origem} 
                tipo={pedido.tipo} 
                taxaEntrega={pedido.taxaEntrega}
                meioPagamento={pedido.meioPagamento}
            />
            <AreaCliente cliente={pedido.cliente} endereco={pedido.endereco} />
        </>
        }
    </Layout >
}

const AreaPedido = ({origem, tipo, taxaEntrega, meioPagamento } : any) => (
    <ContainerDados>
        <Typography color="primary" variant="subtitle1" gutterBottom>Dados do pedido:</Typography>
        <Grid container spacing={3}>
            <Grid item xs={3}>
                <TextField
                    label="Origem"
                    variant="outlined"
                    fullWidth
                    value={origem}
                    InputProps={{
                        readOnly: true,
                    }}
                />
            </Grid>
            <Grid item xs={3}>
                <TextField
                    label="Tipo"
                    variant="outlined"
                    fullWidth
                    value={tipo}
                    InputProps={{
                        readOnly: true,
                    }}
                />
 
            </Grid>
            <Grid item xs={3}>
                <TextField
                    label="Meio de Pagamento"
                    variant="outlined"
                    fullWidth
                    value={meioPagamento}
                    InputProps={{
                        readOnly: true,
                    }}
                />
            </Grid>
            <Grid item xs={3}>
                <TextField
                    label="Taxa de Entrega"
                    variant="outlined"
                    fullWidth
                    value={formatarValor(taxaEntrega)}
                    InputProps={{
                        readOnly: true,
                    }}
                />
            </Grid>
        </Grid>
    </ContainerDados>
)

interface PropsAreaCliente{
    cliente: Cliente
    endereco: Endereco
}

const AreaCliente = ({cliente, endereco} : PropsAreaCliente) => {

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
                            value={cliente.nome}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="CPF"
                            variant="outlined"
                            fullWidth
                            value={cliente.cpf}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
 
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Telefone/Celular"
                            variant="outlined"
                            fullWidth
                            value={cliente.telefone}
                            InputProps={{
                                readOnly: true,
                            }}
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
                            value={endereco.logradouro}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Complemento"
                            variant="outlined"
                            fullWidth
                            value={endereco.complemento}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Bairro"
                            variant="outlined"
                            fullWidth
                            value={endereco.bairro}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            label="Estado"
                            variant="outlined"
                            fullWidth
                            value={endereco.estado}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={3}>
                        <TextField
                            label="Cidade"
                            variant="outlined"
                            fullWidth
                            value={endereco.cidade}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TextField
                            label="Observação"
                            variant="outlined"
                            fullWidth
                            value={endereco.observacao}
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                    </Grid>
                </Grid>
            </ContainerDados>
        </>
    )
}


export default PaginaPedidos