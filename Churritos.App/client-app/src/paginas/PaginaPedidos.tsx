import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'

const PaginaPedidos = () => {
    const [pedidos, setPedidos] = useState([]);

    useEffect(() => {
        fetch("/api/pedidos")
            .then(res => res.json())
            .then(data => setPedidos(data))
    }, []);

    return <Layout pagename="Pedidos">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Data de criação', field: 'dataCriacao' },
                { title: 'Quantidade', field: 'quantidade' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={pedidos}
            title="Lista de pedidos"
            options={{search: false}}
            />
    </Layout>
}


export default PaginaPedidos