import React, { useEffect, useState, useMemo, useCallback } from 'react'
import { Adicional, Produto } from '../pedido/Models'
import {
    Checkbox,
    FormControlLabel,
    TextField,
    Accordion,
    AccordionSummary,
    AccordionDetails,
    Typography,
    ListItem,
    MenuItem,
    Chip,
    Paper,
    Button,
    Grid,
    LinearProgress,
    Fade
} from '@material-ui/core'
import { formatarValor } from '../../utils'
import Layout from '../Layout'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import NumberFormat from 'react-number-format'
import styled from 'styled-components'

interface VinculoAdicional {
    adicional: Adicional,
    vinculado: boolean
}

const NumberFormatCustom = (props: any) => {
    const { inputRef, onChange, ...other } = props

    return (
        <NumberFormat
            {...other}
            getInputRef={inputRef}
            onValueChange={(values) => onChange(values.value)}
            thousandSeparator="."
            decimalSeparator=","
            isNumericString
            fixedDecimalScale
            decimalScale={2}
            prefix="R$ "
            allowNegative={false}
        />
    )
}

interface Categoria {
    id: number,
    nome: string
}

const ContainerEdicao = styled(Paper)`
    display:flex;
    padding: 10px;
    margin-bottom: 10px;
    align-items: center;
    justify-content: space-between;

`

const ContainerDados = styled(Paper)`
    padding: 15px;
`

const TituloPainel = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
`

const SecaoEdicao = styled.div`
  margin: 10px;
`

const CampoTextoComValidacao = (props: any) => {
    const campoValido = props.value ? props.value.toString().length > 0 : false

    return <TextField
        {...props}
        error={!campoValido}
        helperText={!campoValido ? 'Campo inválido' : null}
    />
}

const PaginaCriarProduto = ({ match: { params }, history }: any) => {
    const [adicionais, setAdicionais] = useState<VinculoAdicional[]>([])
    const [produto, setProduto] = useState<Produto>()
    const [todasAsCategorias, setTodasAsCategorias] = React.useState<Categoria[]>([])
    const [categoriaSelecionada, setCategoriaSelecionada] = React.useState<number>(1)
    const [loading, setLoading] = React.useState<boolean>(true)


    useEffect(() => {
        setLoading(true)
        Promise.all([
            fetch('/api/categoria')
                .then(res => res.json())
                .then(data => {
                    setTodasAsCategorias(data)
                }),
            fetch('/api/adicional')
                .then(res => res.json())
                .then(data => {
                    setAdicionais(data.map((a: Adicional) => ({adicional: a, vinculado: false })))
                }).then(() => setLoading(false))
        ])    
    }, [])


    const criarProduto = async () => {
        const rawResponse = await fetch('/api/produto', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    ...produto, categoriaSelecionada, adicionaisVinculados: adicionais
                })
        })

        if (rawResponse.status === 200) {
            history.push('/produtos')
        }
    }

    const handleChange = (prop: keyof Produto) => (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        console.log(event)
        if (event.target)
            setProduto({ ...(produto as Produto), [prop]: event.target.value })
        else
            setProduto({ ...(produto as Produto), [prop]: Number(event) })
    }

    const listaAdicionais = adicionais.map(a => a.adicional)

    const alterarVinculo = useCallback((adicional: VinculoAdicional) => {
        const indice = adicionais.indexOf(adicional)
        adicionais[indice].vinculado = !adicional.vinculado
        setAdicionais([...adicionais])
    }, [adicionais])

    const ListItemCheckbox = useMemo(() => ({ vinculo }: any) => {
        const adicional = vinculo.adicional
        return <ListItem key={adicional.id} dense>
            <FormControlLabel
                key={adicional.id}
                control={<Checkbox checked={adicionais.find(vin => vin === vinculo)?.vinculado} value={adicional.id}
                    onChange={() => alterarVinculo(vinculo)}
                />}
                label={`${adicional.nome} ${adicional.valor > 0 ? formatarValor(adicional.valor) : ''}`}
            />
        </ListItem>
    }, [adicionais, alterarVinculo])


    const adicionaisSelecionados = (categoria: string) =>
        adicionais.filter(a => a.adicional.tipo === categoria && a.vinculado).map(a => a.adicional)

    const handleCategoriaChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setCategoriaSelecionada(event.target.value as number)
    }

    return <Layout pagename="Criar produto">
        {loading && <LinearProgress />}
        {!loading &&
            <Fade in>
                <form noValidate autoComplete="off">
                    <ContainerEdicao>
                        <Button variant="contained" color="primary" onClick={() => criarProduto()}>Salvar</Button>
                    </ContainerEdicao>
                    <ContainerDados>
                        <Typography color="secondary" variant="h6" gutterBottom>Dados do produto: </Typography>
                        <SecaoEdicao>                            
                            <Grid container spacing={3}>
                                <Grid item xs={2}>
                                    <TextField
                                        select
                                        label="Categoria"
                                        value={categoriaSelecionada ?? 0}
                                        onChange={handleCategoriaChange}
                                        variant="outlined"
                                        fullWidth
                                    >
                                        {todasAsCategorias.map(categoria => <MenuItem key={categoria.id} value={categoria.id}>{categoria.nome}</MenuItem>)}
                                    </TextField>
                                </Grid>
                                <Grid item xs={6}>
                                    <CampoTextoComValidacao id="nome" label="Nome" name="nome" fullWidth value={produto?.nome} variant="outlined"
                                        onChange={handleChange('nome')}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <CampoTextoComValidacao id="valor" label="Valor" name="valor" fullWidth value={produto?.valor} variant="outlined"
                                        onChange={handleChange('valor')}
                                        InputProps={{
                                            inputComponent: NumberFormatCustom as any,
                                        }} />
                                </Grid>
                            </Grid>
                        </SecaoEdicao>
                        <Typography color="secondary" variant="h6" gutterBottom>Selecionar adicionais: </Typography>

                        <SecaoEdicao>
                            {[...new Set(listaAdicionais.map(a => a.tipo))].map(categoria => {
                                const adicionaisSelecionadosDaCategoria = adicionaisSelecionados(categoria)

                                return <Accordion key={categoria}>
                                    <AccordionSummary expandIcon={<ExpandMoreIcon />} id={`${categoria}-panel`}>
                                        <TituloPainel>
                                            <Typography>
                                                {categoria}
                                            </Typography>
                                            {adicionaisSelecionadosDaCategoria.length > 0 &&
                                                <Chip color="primary" label={`${adicionaisSelecionadosDaCategoria.length} selecionados`} variant="outlined" />}
                                        </TituloPainel>
                                    </AccordionSummary>
                                    <AccordionDetails>
                                        <Grid container spacing={0}>
                                            {adicionais.filter(a => a.adicional.tipo === categoria).map((vinculo: VinculoAdicional) => (
                                                <Grid key={vinculo.adicional.id} item xs={3}>
                                                    <ListItemCheckbox key={vinculo.adicional.id} vinculo={vinculo} />
                                                </Grid>)
                                            )}
                                        </Grid>
                                    </AccordionDetails>
                                </Accordion>
                            }
                            )}
                        </SecaoEdicao>
                    </ContainerDados>
                </form>
            </Fade>
        }

    </Layout >
}

export default PaginaCriarProduto