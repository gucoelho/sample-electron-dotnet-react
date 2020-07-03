import React, { useEffect, useState } from 'react'
import { Adicional } from '../pedido/Models'
import { Checkbox, FormControlLabel } from '@material-ui/core'
import Layout from '../Layout'

interface VilculoAdicional {
    adicional: Adicional,
    vinculado: boolean
}


const VincularAdicionais = ({ match: { params } }: any) => {
    const [adicionais, setAdicionais] = useState([])

    useEffect(() => {

        fetch(`/api/produto/${params.id}/adicionais`)
            .then(res => res.json())
            .then(data => setAdicionais(data))

    }, [params.id])

    return <Layout pagename={`Produto: ${params.id}`} >
        {adicionais.map((a: VilculoAdicional) =>
            <FormControlLabel
                key={a.adicional.id}
                control={
                    <Checkbox
                        checked={a.vinculado} value={a.adicional.id}
                    />
                }
                label={a.adicional.nome}
            />
        )}
    </Layout>
}

export default VincularAdicionais