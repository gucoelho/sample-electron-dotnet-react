import React, { useState, useEffect, ReactElement } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'
import { formatarValor } from '../utils'
import EditIcon from '@material-ui/icons/Edit'
interface Item {
    id: number,
    nome: string,
    valor: number
}


const PaginaProdutos = ({ history }: any) => {
    const [produtos, setProdutos] = useState([])

    useEffect(() => {
        fetch('/api/produto')
            .then(res => res.json())
            .then(data => data.map((item: Item) => ({ ...item, valor: formatarValor(item.valor) })))
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
            options={{
                search: false, pageSize: 10,
                actionsColumnIndex: -1,
            }}
            localization={{
                header: {
                    actions: 'Ações'
                },
                body: {
                    emptyDataSourceMessage: 'Não tem produtos'
                }
            }}
            actions={[
                {
                    icon: 'edit',
                    tooltip: 'Editar produto',
                    onClick: (event, rowData: any) => history.push(`produto/${rowData.id}`)
                }
            ]}
        />
    </Layout>
}


export default PaginaProdutos