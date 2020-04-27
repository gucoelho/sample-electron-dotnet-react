import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'

const PaginaCoberturas = () => {
    const [categorias, setCategorias] = useState([]);

    useEffect(() => {
        fetch("/api/categorias")
            .then(res => res.json())
            .then(data => setCategorias(data))
    }, []);

    return <Layout pagename="Categorias">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Nome', field: 'nome' },
            ]}
            data={categorias}
            title="Lista de categorias"
            options={{search: false}}
            />
    </Layout>
}


export default PaginaCoberturas