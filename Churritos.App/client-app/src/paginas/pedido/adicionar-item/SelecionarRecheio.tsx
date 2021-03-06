import React, { useState, useEffect } from 'react'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core'
import styled from 'styled-components'
import { formatarValor } from '../../../utils'

interface Recheio {
    id: number,
    nome: string,
    valor: number
}

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarRecheio = ({adicionarRecheio, produtoId} : any) => {
    const [recheios, setRecheios] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        fetch(`/api/produto/${produtoId}/recheios`)
            .then(res => res.json())
            .then(data => setRecheios(data))
            .then(() => setLoading(false))
    }, [produtoId])

    return <div>
        {loading && <LinearProgress />}
        {!loading && 
         (<List> 
             {recheios?.map((p : Recheio) => 
                 <SeletorProduto key={p.id}>
                     <ListItem button onClick={() => adicionarRecheio(p)}>
                         {p.valor <= 0 && <ListItemText primary={`${p.nome}`} />}
                         {p.valor > 0 && <ListItemText primary={`${p.nome} + ${formatarValor(p.valor)}`} />}
                     </ListItem>
                 </SeletorProduto>)}
         </List>)}
    </div> 
}


export default SelecionarRecheio