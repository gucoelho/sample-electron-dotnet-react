import React, { useState, useEffect, ChangeEvent } from 'react'
import { Button, LinearProgress, List, ListItemText, ListItem, Paper, Checkbox } from '@material-ui/core'
import styled from 'styled-components'
import { formatarValor } from '../../../utils'
import { Adicional } from '../Models'

type Extra = Adicional

const SeletorExtra = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;

    cursor: pointer;
`
const BotãoAdicionarExtra = styled(Button)`
    background-color: #6c3317;
    color: white;
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

    const mensagemBotão = extrasSelecionados.length > 0 ? `Adicionar ${extrasSelecionados.length} extras` : 'Não adicionar extras'

    return <div>
        {loading && <LinearProgress />}
        {!loading &&
            <>
                <BotãoAdicionarExtra variant="contained" onClick={() => adicionarExtras(extrasSelecionados)} >{mensagemBotão}</BotãoAdicionarExtra>
                <List>
                    {recheios.map((extra: Extra) =>
                        <SeletorExtra key={extra.id}>
                            <ListItem onClick={() => adicionaOuRemoveExtra(extra)}>
                                <Checkbox name={extra.nome} checked={extrasSelecionados.includes(extra)} />
                                {extra.valor <= 0 && <ListItemText primary={`${extra.nome}`} />}
                                {extra.valor > 0 && <ListItemText primary={`${extra.nome} + ${formatarValor(extra.valor)}`} />}
                            </ListItem>
                        </SeletorExtra>)}
                </List> 
            </>}
    </div>
}


export default SelecionarExtras