import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import MaterialTable from 'material-table'
import Button from '@material-ui/core/Button';
import { Link } from 'react-router-dom';

const PaginaPedidos = () => {
    const [pedidos, setPedidos] = useState([]);

    useEffect(() => {
        fetch("/api/pedidos")
            .then(res => res.json())
            .then(data => setPedidos(data))
    }, []);

    return <Layout pagename="Pedidos">
        <Button component={Link} to="/pedidos/criar" >Novo pedido</Button>
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