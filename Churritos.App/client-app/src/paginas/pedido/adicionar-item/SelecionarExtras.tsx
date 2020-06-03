import React, { useState, useEffect, ChangeEvent } from 'react'
import { Button, LinearProgress, List, ListItemText, ListItem, Paper, Checkbox } from '@material-ui/core'
import styled from 'styled-components'
import { formatarValor } from '../../../utils'
import { Adicional } from '../Models'

type Extra = Adicional

const SeletorProduto = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
`

const SelecionarExtras = ({ adicionarExtras, produtoId }: any) => {
    const [recheios, setRecheios] = useState([])
    const [loading, setLoading] = useState(false)
    const [extrasSelecionados, setExtrasSelecionados] = useState<Extra[]>([])

    useEffect(() => {
        setLoading(true)
        fetch(`/api/produto/${produtoId}/extras`)
            .then(res => res.json())
            .then(data => setRecheios(data))
            .then(() => setLoading(false))
    }, [produtoId])

    const adicionaOuRemoveExtra = (extra: Extra) => {
        if (extrasSelecionados.includes(extra))
            setExtrasSelecionados(extrasSelecionados.filter(e => e.id !== extra.id))
        else
            setExtrasSelecionados([...extrasSelecionados, extra])
    }

    return <div>
        {loading && <LinearProgress />}
        {!loading &&
            <>
                <Button onClick={() => adicionarExtras(extrasSelecionados)} >Adicionar</Button>
                <List>
                    {recheios.map((extra: Extra) =>
                        <SeletorProduto key={extra.id}>
                            <ListItem>
                                <Checkbox name={extra.nome} checked={extrasSelecionados.includes(extra)} onChange={(event: ChangeEvent, checked: boolean) => adicionaOuRemoveExtra(extra)} />
                                {extra.valor <= 0 && <ListItemText primary={`${extra.nome}`} />}
                                {extra.valor > 0 && <ListItemText primary={`${extra.nome} + ${formatarValor(extra.valor)}`} />}
                            </ListItem>
                        </SeletorProduto>)}
                </List> </>}
    </div>
}


export default SelecionarExtras