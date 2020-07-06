import React, { useState, useEffect } from 'react'
import { Button, LinearProgress, ListItemText, Paper, Checkbox, Grid } from '@material-ui/core'
import styled from 'styled-components'
import { formatarValor } from '../../../utils'
import { Adicional } from '../Models'

type Extra = Adicional

const SeletorExtra = styled(Paper)`
    margin: 0.6rem 0;
    border-radius: 0;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    cursor: pointer;
`
const BotãoAdicionarExtra = styled(Button)`
    background-color: #6c3317;
    color: white;
`

const SelecionarExtras = ({ adicionarExtras, produtoId }: any) => {
    const [extras, setExtras] = useState<Adicional[]>([])
    const [loading, setLoading] = useState(false)
    const [extrasSelecionados, setExtrasSelecionados] = useState<Extra[]>([])

    useEffect(() => {
        setLoading(true)
        fetch(`/api/produto/${produtoId}/extras`)
            .then(res => res.json())
            .then(data => setExtras(data))
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
                <Grid container spacing={3}>
                    {extras.map((extra: Extra) =>
                        <Grid key={extra.id} item xs={3} onClick={() => adicionaOuRemoveExtra(extra)}>
                            <SeletorExtra key={extra.id}>
                                <Checkbox name={extra.nome} checked={extrasSelecionados.includes(extra)} />
                                {extra.valor <= 0 && <ListItemText primary={`${extra.nome}`} />}
                                {extra.valor > 0 && <ListItemText primary={`${extra.nome} + ${formatarValor(extra.valor)}`} />}
                            </SeletorExtra>
                        </Grid>

                    )}

                </Grid>
            </>
        }
    </div >
}


export default SelecionarExtras