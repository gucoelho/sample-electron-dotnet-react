import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import MaterialTable from 'material-table'
import Button from '@material-ui/core/Button'
import { Link } from 'react-router-dom'
import { formatarValor } from '../../utils'
import { CSVLink } from 'react-csv'
import styled from 'styled-components'
import { DatePicker } from '@material-ui/pickers'
import moment, { Moment } from 'moment'

interface Pedido {
    id: number,
    nome: string,
    quantidade: number,
    valor: number
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
`

const PaginaPedidos = () => {
    const [pedidos, setPedidos] = useState([])
    const [pedidosDownload, setPedidosDownload] = useState([])
    const [relatórioGerado, setRelatórioGerado] = useState(true)
    const [dataSelecionada, setDataSelecionada] = useState<Moment | null>(moment())

    useEffect(() => {
        setRelatórioGerado(false)

        fetch(`/api/pedido/${dataSelecionada?.toISOString()}`)
            .then(res => res.json())
            .then(data => setPedidos(data))

        fetch(`/api/pedido/download/${dataSelecionada?.toISOString()}`)
            .then(res => res.json())
            .then(data => setPedidosDownload(data))
            .then(() => setRelatórioGerado(true))

    }, [dataSelecionada])

    return <Layout pagename="Pedidos">
        <Button component={Link} to="/pedidos/criar" >Novo pedido</Button>

        <DatePicker value={dataSelecionada}
            inputVariant="outlined"
            variant="inline"
            format="DD/MM/yyyy"
            autoOk
            onChange={(date) => setDataSelecionada(date)} />

        {relatórioGerado &&
            <BotãoBaixarRelatório
                data={pedidosDownload}
                target="_blank"
                headers={csvHeaders}
                filename={`relatorio-${new Date().toISOString()}.csv`}
                onClick={() => setRelatórioGerado(true)}
            > Baixar</BotãoBaixarRelatório>
        }

        {formatarValor(pedidos.map((x: Pedido) => x.valor).reduce((acc, a) => acc + a, 0))}

        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Data de criação', field: 'dataCriacao' },
                { title: 'Quantidade', field: 'quantidade' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={pedidos.map((p: Pedido) => ({ ...p, valor: formatarValor(p.valor) }))}
            title="Lista de pedidos"
            options={{ search: false, pageSize: 10 }}

        />
    </Layout>
}


export default PaginaPedidos