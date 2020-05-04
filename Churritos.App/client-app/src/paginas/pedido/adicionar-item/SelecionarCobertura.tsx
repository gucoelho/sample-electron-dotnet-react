import React, { useState, useEffect } from 'react'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core';
import styled from 'styled-components'

interface Cobertura {
    id: number,
    nome: string
}

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarCobertura = ({adicionarCobertura} : any) => {
    const [coberturas, setCoberturas] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch("/api/cobertura")
            .then(res => res.json())
            .then(data => setCoberturas(data))
            .then(() => setLoading(false))
    }, [])

   return <div>
     {loading && <LinearProgress />}
     {!loading && 
         (<List> 
             {coberturas.map((p : Cobertura) => 
             <SeletorProduto key={p.id}>
                <ListItem button onClick={() => adicionarCobertura(p)}>
                    <ListItemText primary={`${p.nome}`} />
                </ListItem>
            </SeletorProduto>)}
         </List>)}
   </div> 
}


export default SelecionarCobertura