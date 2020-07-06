import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import TabelaPedido from './TabelaPedido'
import { PedidoDetalhe } from './Models'
import { LinearProgress } from '@material-ui/core'

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
        {!loading && pedido && <TabelaPedido pedido={pedido} />}
    </Layout >
}


export default PaginaPedidos