import React, { useEffect, useState } from 'react'
import { Adicional, Produto } from '../pedido/Models'
import {
    Checkbox,
    FormControlLabel,
    TextField,
    ExpansionPanel,
    ExpansionPanelSummary,
    ExpansionPanelDetails,
    Typography,
    List,
    ListItem,
    ListSubheader
} from '@material-ui/core'
import Layout from '../Layout'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import NumberFormat from 'react-number-format'

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
            prefix="R$ "
            allowNegative={false}
        />
    )
}


const nenhumItemComponent = <ListItem><Typography>Nenhum item </Typography></ListItem>

const PaginaEditarProduto = ({ match: { params } }: any) => {
    const [adicionais, setAdicionais] = useState<VinculoAdicional[]>([])
    const [produto, setProduto] = useState<Produto>()

    useEffect(() => {
        fetch(`/api/produto/${params.id}`)
            .then(res => res.json())
            .then(data => {
                setAdicionais(data.adicionaisVinculados)
                setProduto({
                    id: data.id,
                    categoriaId: data.categoria,
                    nome: data.nome,
                    valor: data.valor
                })
            })
    }, [params.id])

    const handleChange = (prop: keyof Produto) => (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        console.log(prop, event)
        if (event.target)
            setProduto({ ...(produto as Produto), [prop]: event.target.value })
        else
            setProduto({ ...(produto as Produto), [prop]: event })
    }

    const listaAdicionais = adicionais.map(a => a.adicional)

    const alterarVinculo = (adicional: Adicional) => {
        const item = adicionais.find(vin => vin.adicional === adicional)
        if (item) {
            adicionais[adicionais.indexOf(item)].vinculado = !item.vinculado
            setAdicionais([...adicionais])
        }
    }

    const ListItemCheckbox = ({ adicional }: any) =>
        <ListItem key={adicional.id} dense>
            <FormControlLabel
                key={adicional.id}
                control={<Checkbox checked={adicionais.find(vin => vin.adicional === adicional)?.vinculado} value={adicional.id}
                    onChange={() => alterarVinculo(adicional)}
                />}
                label={adicional.nome}
            />
        </ListItem>

    const adicionaisSelecionados = (categoria: string) =>
        adicionais.filter(a => a.adicional.tipo === categoria && a.vinculado).map(a => a.adicional)

    const adicionaisNaoSelecionados = (categoria: string) =>
        adicionais.filter(a => a.adicional.tipo === categoria && !a.vinculado).map(a => a.adicional)


    return <Layout pagename={`Editar produto: ${params.id}`}>

        <form noValidate autoComplete="off">
            <TextField id="nome" label="Nome" name="nome" value={produto?.nome} variant="standard" defaultValue=" "
                onChange={handleChange('nome')}
            />
            <TextField id="valor" label="Valor" name="valor" value={produto?.valor} variant="standard"
                onChange={handleChange('valor')}
                InputProps={{
                    inputComponent: NumberFormatCustom as any,
                }}
                defaultValue={0} />
        </form>

        <Typography>Editar adicionais: </Typography>
        {[...new Set(listaAdicionais.map(a => a.tipo))].map(categoria => {
            const adicionaisSelecionadosDaCategoria = adicionaisSelecionados(categoria)
            const adicionaisNaoSelecionadosDaCategoria = adicionaisNaoSelecionados(categoria)

            return <ExpansionPanel key={categoria}>
                <ExpansionPanelSummary expandIcon={<ExpandMoreIcon />} id={`${categoria}-panel`}>
                    <Typography>
                        {categoria}{'  '}
                    </Typography>
                    {adicionaisSelecionadosDaCategoria.length > 0 &&
                        <Typography variant="caption">
                            {`${adicionaisSelecionadosDaCategoria.length} selecionados`}
                        </Typography>}
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <List
                        subheader={<ListSubheader component="div"> Não selecionados: </ListSubheader>}>
                        {adicionaisNaoSelecionadosDaCategoria.length <= 0 && nenhumItemComponent}
                        {adicionaisNaoSelecionadosDaCategoria
                            .map(adicional => <ListItemCheckbox key={adicional.id} adicional={adicional} />)}
                    </List>
                    <List subheader={<ListSubheader component="div"> Selecionados: </ListSubheader>}>
                        {adicionaisSelecionadosDaCategoria.length <= 0 && nenhumItemComponent}
                        {adicionaisSelecionadosDaCategoria
                            .map(adicional => <ListItemCheckbox key={adicional.id} adicional={adicional} />)}
                    </List>
                </ExpansionPanelDetails>
            </ExpansionPanel>
        }
        )}
    </Layout >
}

export default PaginaEditarProduto