import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'
import { formatarValor } from '../utils'

interface Adicionais {
    id: number,
    nome: string,
    valor: number
}

const PaginaAdicionais = () => {
    const [adicionais, setAdicionais] = useState([])

    useEffect(() => {
        fetch('/api/adicional')
            .then(res => res.json())
            .then(data => setAdicionais(data))
    }, [])

    return <Layout pagename="Adicionais">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Tipo', field: 'tipo' },
                { title: 'Nome', field: 'nome' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={adicionais.map((item: Adicionais) => ({ ...item, valor: formatarValor(item.valor) }))}
            title="Lista de adicionais"
            options={{ search: false, pageSize: 10 }}
        />
    </Layout>
}


export default PaginaAdicionais