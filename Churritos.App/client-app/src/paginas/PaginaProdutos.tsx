import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'
import { formatarValor } from '../utils'
interface Item {
    id: number,
    nome: string,
    valor: number
}


const PaginaProdutos = () => {
    const [produtos, setProdutos] = useState([])

    useEffect(() => {
        fetch('/api/produto')
            .then(res => res.json())
            .then(data => data.map((item: Item) => ({...item, valor: formatarValor(item.valor)})))
            .then(data => setProdutos(data))
    }, [])

    return <Layout pagename="Produtos">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Nome', field: 'nome' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={produtos}
            title="Lista dos produtos"
            options={{search: false, pageSize: 10}}
        />
    </Layout>
}


export default PaginaProdutos