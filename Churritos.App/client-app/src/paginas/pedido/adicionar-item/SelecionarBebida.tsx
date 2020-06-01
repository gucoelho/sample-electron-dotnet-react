import React, { useState, useEffect } from 'react'
import { LinearProgress, List, ListItemText, ListItem, Paper} from '@material-ui/core';
import styled from 'styled-components'
import {formatarValor} from '../../../utils';

interface Bebida {
    id: number,
    nome: string,
    valor: number
}

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarBebidas = ({ adicionarItem } : any) => {
    const [bebidas, setBebidas] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch(`/api/produto/bebidas`)
            .then(res => res.json())
            .then(data => setBebidas(data))
            .then(() => setLoading(false))
    }, [])

   return <div>
     {loading && <LinearProgress />}
     {!loading && 
         (<List> 
             {bebidas.map((p : Bebida) => 
             <SeletorProduto key={p.id}>
                <ListItem button onClick={() => adicionarItem(p)}>
                   {p.valor > 0 && <ListItemText primary={`${p.nome} + ${formatarValor(p.valor)}`} />}
                </ListItem>
            </SeletorProduto>)}
         </List>)}
   </div> 
}


export default SelecionarBebidas