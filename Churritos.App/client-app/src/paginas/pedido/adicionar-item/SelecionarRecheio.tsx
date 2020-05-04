import React, { useState, useEffect } from 'react'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core';
import styled from 'styled-components'

interface Recheio {
    id: number,
    nome: string
}

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarRecheio = ({adicionarRecheio, categoriaId} : any) => {
    const [recheios, setRecheios] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch(`/api/recheio/${categoriaId}`)
            .then(res => res.json())
            .then(data => setRecheios(data))
            .then(() => setLoading(false))
    }, [categoriaId])

   return <div>
     {loading && <LinearProgress />}
     {!loading && 
         (<List> 
             {recheios.map((p : Recheio) => 
             <SeletorProduto key={p.id}>
                <ListItem button onClick={() => adicionarRecheio(p)}>
                    <ListItemText primary={`${p.nome}`} />
                </ListItem>
            </SeletorProduto>)}
         </List>)}
   </div> 
}


export default SelecionarRecheio