import React, { useState, useEffect } from 'react'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core';
import styled from 'styled-components'
import {formatarValor} from '../../../utils';

interface Cobertura {
    id: number,
    nome: string,
    valor: number
}

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarCobertura = ({adicionarCobertura, produtoId } : any) => {
    const [coberturas, setCoberturas] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch(`/api/produto/${produtoId}/coberturas`)
            .then(res => res.json())
            .then(data => setCoberturas(data))
            .then(() => setLoading(false))
    }, [produtoId])

   return <div>
     {loading && <LinearProgress />}
     {!loading && 
         (<List> 
             {coberturas.map((p : Cobertura) => 
             <SeletorProduto key={p.id}>
                <ListItem button onClick={() => adicionarCobertura(p)}>
                   {p.valor <= 0 && <ListItemText primary={`${p.nome}`} />}
                   {p.valor > 0 && <ListItemText primary={`${p.nome} + ${formatarValor(p.valor)}`} />}
                </ListItem>
            </SeletorProduto>)}
         </List>)}
   </div> 
}


export default SelecionarCobertura