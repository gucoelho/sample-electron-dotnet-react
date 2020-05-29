import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'

const PaginaCoberturas = () => {
    const [coberturas, setCoberturas] = useState([]);

    useEffect(() => {
        fetch("/api/cobertura")
            .then(res => res.json())
            .then(data => setCoberturas(data))
    }, []);

    return <Layout pagename="Coberturas">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Nome', field: 'nome' },
                { title: '', field: '' },
                { title: '', field: '' },
            ]}
            data={coberturas}
            title="Lista de coberturas"
            options={{search: false}}
            />
    </Layout>
}


export default PaginaCoberturas