import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import TabelaPedido from './TabelaPedido'
import { ItemPedido } from './Models'

interface Pedido {
    itens: ItemPedido[]
    desconto: number
}


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
    const [loading, setLoading] = useState(false)

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