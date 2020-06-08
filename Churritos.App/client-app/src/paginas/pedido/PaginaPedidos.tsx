import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import MaterialTable from 'material-table'
import Button from '@material-ui/core/Button'
import { Link } from 'react-router-dom'
import { formatarValor } from '../../utils'
import { CSVLink } from 'react-csv'

// TODO: Unificar Itens
interface Pedido {
    id: number,
    nome: string,
    quantidade: number,
    valor: number
}

const PaginaPedidos = () => {
    const [pedidos, setPedidos] = useState([])
    const [pedidosDownload, setPedidosDownload] = useState([])
    const [relatórioGerado, setRelatórioGerado] = useState(false)

    useEffect(() => {
        fetch('/api/pedido')
            .then(res => res.json())
            .then(data => setPedidos(data))
    }, [])

    const gerarRelatorio = () => {
        setRelatórioGerado(false)
        fetch('/api/pedido/download')
            .then(res => res.json())
            .then(data => setPedidosDownload(data))
            .then(() => setRelatórioGerado(true))
    }

    return <Layout pagename="Pedidos">
        <Button component={Link} to="/pedidos/criar" >Novo pedido</Button>

        {relatórioGerado &&
            <CSVLink
                data={pedidosDownload}
                target="_blank"
                filename={`relatorio-${new Date().toISOString()}.csv`}
                onClick={() => setRelatórioGerado(false)}
            > Baixar</CSVLink>
        }

        {!relatórioGerado &&
            <Button onClick={gerarRelatorio}>Gerar Relatório</Button>}

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