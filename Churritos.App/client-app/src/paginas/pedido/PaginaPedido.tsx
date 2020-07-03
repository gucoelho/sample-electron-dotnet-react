import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import MaterialTable, { MTableToolbar } from 'material-table'
import Button from '@material-ui/core/Button'
import { Link } from 'react-router-dom'
import { formatarValor } from '../../utils'
import { CSVLink } from 'react-csv'
import styled from 'styled-components'
import { DatePicker } from '@material-ui/pickers'
import moment, { Moment } from 'moment'
import { Paper, Typography } from '@material-ui/core'
import TabelaPedido from './TabelaPedido'
import { ItemPedido } from './Models'

interface Pedido {
    itens: ItemPedido[]
    desconto: number
}

const csvHeaders = [
    { label: 'Id do Pedido', key: 'pedidoId' },
    { label: 'Data do Pedido', key: 'data' },
    { label: 'Nome do Produto', key: 'nomeProduto' },
    { label: 'Id do adicional', key: 'adicionalId' },
    { label: 'Nome do adicional', key: 'adicionalNome' },
    { label: 'Valor', key: 'valor' },
]

const BotãoBaixarRelatório = styled(CSVLink)`
    color: rgba(0, 0, 0, 0.87);
    padding: 6px 16px;
    font-size: 0.875rem;
    min-width: 64px;
    box-sizing: border-box;
    font-weight: 500;
    line-height: 1.75;
    border-radius: 4px;
    text-transform: uppercase;
    text-decoration: none;
    background-color: #1f8b57;
    color: white;
    
    position: relative;
    display: flex;
    height: 35px;
    width: 150px;
    padding: 5px;
    align-items: center;

    &:hover {
        background-color: #10613a;
        transition: background-color 0.2s ease-in;
    }
`

const Icone = styled.img`
    position:relative;
    height: 90%;
    width: auto;
    margin: 2px 10px;
`

const ActionBar = styled(Paper)`
    display: flex;
    align-items: center;
    justify-content: space-around;
    padding: 10px;
    margin-bottom: 10px;
`

const CampoData = styled(DatePicker)`
  & .MuiOutlinedInput-input {
        padding: 10px;
  }
`

const TableToolbar = styled.div`
    display:flex;
    justify-content: space-between;
    align-items: center;

    & .MTableToolbar-root-225 {
        flex: 1;
    }
`

const ValorTotal = styled(Typography)`
    flex: 1;
    align-content: flex-end;
    justify-content: flex-end;
    align-items: flex-end;
    display: flex;
    padding-right: 24px;
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
    const [pedido, setPedido] = useState<Pedido>()
    const [pedidosDownload, setPedidosDownload] = useState([])
    const [relatórioGerado, setRelatórioGerado] = useState(true)
    const [loading, setLoading] = useState(false)
    const [dataSelecionada, setDataSelecionada] = useState<Moment | null>(moment())

    useEffect(() => {
        setLoading(true)

        fetch(`/api/pedido/${params.id}`)
            .then(res => res.json())
            .then(data => setPedido(data))
            .then(() => setLoading(false))

    }, [params.id])

    return <Layout pagename={`Pedido ${params.id}`} > {
        pedido && <TabelaPedido itens={pedido.itens} desconto={pedido.desconto} />
    }
    </Layout >
}


export default PaginaPedidos