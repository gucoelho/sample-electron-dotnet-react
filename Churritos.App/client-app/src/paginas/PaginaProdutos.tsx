import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'
import { formatarValor } from '../utils'
import {Button } from '@material-ui/core'
import styled from 'styled-components'

interface Item {
    id: number,
    nome: string,
    valor: number
}

const EditarBotão= styled(Button)`

    .MuiIconButton-root:hover {
        background-color: transparent;
    }
`

const CustomTable = styled(MaterialTable) `
    button.MuiButtonBase-root.MuiIconButton-root.MuiIconButton-colorInherit {
            background-color: transparent;
    }
    
`

const PaginaProdutos = ({ history }: any) => {
    const [produtos, setProdutos] = useState([])

    useEffect(() => {
        fetch('/api/produto')
            .then(res => res.json())
            .then(data => data.map((item: Item) => ({ ...item, valor: formatarValor(item.valor) })))
            .then(data => setProdutos(data))
    }, [])

    return <Layout pagename="Produtos">
        <CustomTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Categoria', field: 'categoria' },
                { title: 'Nome', field: 'nome' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={produtos}
            title="Lista dos produtos"
            options={{
                search: false, pageSize: 10,
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
                    icon: () => (<EditarBotão color="primary" variant="outlined">Editar</EditarBotão> as React.ReactElement),
                    tooltip: 'Editar produto',
                    onClick: (event, rowData: any) => history.push(`produto/${rowData.id}`)
                }
            ]}
        />
    </Layout>
}


export default PaginaProdutos