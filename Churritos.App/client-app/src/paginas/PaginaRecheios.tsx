import React, { useState, useEffect } from 'react'
import Layout from './Layout'
import MaterialTable from 'material-table'

const PaginaCoberturas = () => {
    const [coberturas, setCoberturas] = useState([]);

    useEffect(() => {
        fetch("/api/recheio")
            .then(res => res.json())
            .then(data => setCoberturas(data))
    }, []);

    return <Layout pagename="Recheios">
        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Nome', field: 'nome' },
                { title: '', field: '' },
                { title: '', field: '' },
            ]}
            data={coberturas}
            title="Lista de recheios"
            options={{search: false}}
            />
    </Layout>
}


export default PaginaCoberturas