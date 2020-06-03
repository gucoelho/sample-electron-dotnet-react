import React, { useState, useEffect } from 'react'
import { formatarValor } from '../../../utils'
import { LinearProgress, List, ListItemText, ListItem, Paper } from '@material-ui/core'
import styled from 'styled-components'
import { Produto, Adicional } from '../Models'

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarProduto = ({ adicionarItem }: any) => {
    const [produtos, setProdutos] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        fetch('/api/produto/churros')
            .then(res => res.json())
            .then(data => setProdutos(data))
            .then(() => setLoading(false))
    }, [])

    return <div>
        {loading && <LinearProgress />}
        {!loading &&
            (<List>
                {produtos.map((produto: Produto) =>
                    <SeletorProduto key={produto.id}>
                        <ListItem button onClick={() => adicionarItem(produto)}>
                            <ListItemText primary={`${produto.nome} + ${formatarValor(produto.valor)}`} />
                        </ListItem>
                    </SeletorProduto>)}
            </List>)}
    </div>
}


export default SelecionarProduto