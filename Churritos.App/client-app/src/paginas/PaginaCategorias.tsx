import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'

const PaginaCoberturas = () => {
    const [categorias, setCategorias] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        fetch('/api/categoria')
            .then(res => res.json())
            .then(data => setCategorias(data))
            .then(() => setLoading(false))
    }, [])

    return <Layout pagename="Categorias">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Nome', field: 'nome' },
                { title: '', field: '' },
                { title: '', field: '' },
            ]}
            data={categorias}
            title="Lista de categorias"
            options={{ search: false, pageSize: 5 }}
            isLoading={loading}
        />
    </Layout>
}


export default PaginaCoberturas