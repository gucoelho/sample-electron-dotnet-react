import React, { useState, useEffect } from 'react'
import {formatarValor} from '../../../utils'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core';
import styled from 'styled-components'
import Item from '../Item'

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarProduto = ({adicionarItem} : any) => {
    const [produtos, setProdutos] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch("/api/produto")
            .then(res => res.json())
            .then(data => setProdutos(data))
            .then(() => setLoading(false))
    }, [])

   return <div>
     {loading && <LinearProgress />}
     {!loading && 
         (<List> 
             {produtos.map((p :Item) => 
             <SeletorProduto key={p.id}>
                <ListItem button onClick={() => adicionarItem(p)}>
                    <ListItemText primary={`${p.nome} - ${formatarValor(p.valor)}`} />
                </ListItem>
            </SeletorProduto>)}
         </List>)}
   </div> 
}


export default SelecionarProduto