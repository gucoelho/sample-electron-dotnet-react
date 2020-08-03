import React, { useState, useEffect, useCallback } from 'react'
import Button from '@material-ui/core/Button'
import Stepper from '@material-ui/core/Stepper'
import Step from '@material-ui/core/Step'
import StepLabel from '@material-ui/core/StepLabel'
import Typography from '@material-ui/core/Typography'
import { Produto, Adicional } from '../Models'
import SelecionarProduto from './SelecionarProduto'
import SelecionarBebida from './SelecionarBebida'
import SelecionarCobertura from './SelecionarCobertura'
import SelecionarRecheio from './SelecionarRecheio'
import SelecionarExtras from './SelecionarExtras'
import AppBar from '@material-ui/core/AppBar'
import Tabs from '@material-ui/core/Tabs'
import Tab from '@material-ui/core/Tab'
import Box from '@material-ui/core/Box'
import styled from 'styled-components'
import { LinearProgress } from '@material-ui/core'
import ActionBar from '../../../components/ActionBar'

function getSteps() {
    return ['Selecionar produto', 'Selecionar recheio', 'Selecionar cobertura', 'Selecionar extra']
}

function getStepContent(step: number) {
    switch (step) {
    case 0:
        return 'Selecione o produto:'
    case 1:
        return 'Selecione o recheio:'
    case 2:
        return 'Selecione a cobertura:'
    case 3:
        return 'Selecione extras:'
    default:
        return 'Unknown step'
    }
}

const Container = styled.div`
    display: flex;
    padding: 1rem 0;
`

const BotaoVoltar = styled(Button)`
    margin-right: 10px;
`

function HorizontalLinearStepper({ activeStep, botaoVoltar }: any) {
    const steps = getSteps()

    return (
        <div>
            <Stepper activeStep={activeStep}>
                {steps.map((label) => {
                    const stepProps: { completed?: boolean } = {}
                    const labelProps: { optional?: React.ReactNode } = {}
                    return (
                        <Step key={label} {...stepProps}>
                            <StepLabel {...labelProps}>{label}</StepLabel>
                        </Step>
                    )
                })}
            </Stepper>
            <div>
                {activeStep !== steps.length && (
                    <>
                        <Container>
                            {botaoVoltar} {' '} <Typography variant="h6">{getStepContent(activeStep)}</Typography>
                        </Container>

                    </>)
                }
            </div>
        </div>
    )
}

interface TabPanelProps {
    children?: React.ReactNode;
    index: any;
    value: any;
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box p={1}>
                    {children}
                </Box>
            )}
        </div>
    )
}

type Cobertura = Adicional
type Recheio = Adicional

interface Categoria {
    id: number,
    nome: number
}

const ControleEtapas = ({ adicionarItemPedido, cancelarAdicao }: any) => {
    const [etapa, setEtapa] = useState(0)
    const [item, setItem] = useState<Produto>()
    const [cobertura, setCobertura] = useState<Adicional>()
    const [recheio, setRecheio] = useState<Adicional>()
    const [valorAba, setValorAba] = useState(0)
    const [categorias, setCategorias] = useState<Categoria[]>()
    const [extras, setExtras] = useState<Adicional[]>()
    const [loading, setLoading] = useState(false)

    const finalizarItem = useCallback(() => {
        const adicionais: Adicional[] = []

        if (cobertura)
            adicionais.push(cobertura)

        if (recheio)
            adicionais.push(recheio)

        if (extras && extras?.length > 0)
            adicionais.push(...extras)

        adicionarItemPedido({ produto: item, adicionais })
    }, [cobertura, recheio, extras, adicionarItemPedido, item])

    useEffect(() => {
        setLoading(true)
        fetch('/api/categoria')
            .then(res => res.json())
            .then(data => setCategorias(data))
            .then(() => setValorAba(0))
            .then(() => setLoading(false))
    }, [])

    useEffect(() => {
        if (extras && extras.length > 0)
            finalizarItem()
    }, [extras, finalizarItem])

    const handleChange = (event: React.ChangeEvent<unknown>, newValue: number) => {
        setEtapa(0)
        setItem(undefined)
        setCobertura(undefined)
        setRecheio(undefined)
        setExtras([])
        setValorAba(newValue)
    }

    const cancelar = (event: React.MouseEvent) => {
        handleChange(event, 0)
        cancelarAdicao()
    }


    const adicionarItem = (i: Produto) => {
        setItem(i)
        proximaEtapa()
    }

    const adicionarCobertura = (i: Cobertura) => {
        setCobertura(i)
        proximaEtapa()
    }

    const adicionarRecheio = (i: Recheio) => {
        setRecheio(i)
        proximaEtapa()
    }

    const proximaEtapa = () =>
        setEtapa((prevActiveStep: number) => prevActiveStep + 1)

    const voltarEtapa = () =>
        setEtapa((prevActiveStep: number) => prevActiveStep - 1)

    const adicionarBebida = (bebida: Produto) => {
        adicionarItemPedido({ produto: bebida })
    }

    const adicionarExtras = (extras: Adicional[]) => {
        setExtras(extras)
    }


    return (
        <>
            {loading && <LinearProgress />}
            {!loading && <>

                <ActionBar style={{ justifyContent: 'flex-end'}}>
                    <Button onClick={cancelar} color="primary" variant="outlined">
                        Cancelar
                    </Button>
                </ActionBar>
                <AppBar position="static">
                    <Tabs value={valorAba} onChange={handleChange} aria-label="simple tabs example">
                        {categorias && categorias.map((c: Categoria) => <Tab key={`tab-${c.nome}`} label={c.nome} />)}
                    </Tabs>
             
                </AppBar>
                <TabPanel value={valorAba} index={0}>
                    <HorizontalLinearStepper
                        activeStep={etapa}
                        setActiveStep={setEtapa}
                        botaoVoltar={
                            etapa > 0 && <BotaoVoltar variant="outlined" onClick={voltarEtapa}>Voltar</BotaoVoltar>
                        }
                    />
                    {(etapa === 0) && (<SelecionarProduto adicionarItem={adicionarItem} />)}
                    {(etapa === 1) && (<SelecionarRecheio adicionarRecheio={adicionarRecheio} produtoId={item?.id} />)}
                    {(etapa === 2) && (<SelecionarCobertura adicionarCobertura={adicionarCobertura} produtoId={item?.id} />)}
                    {(etapa === 3) && (<SelecionarExtras adicionarExtras={(extrasSelecionados: any) => {
                        if (extrasSelecionados && extrasSelecionados.length > 0)
                            adicionarExtras(extrasSelecionados)
                        else
                            finalizarItem()
                    }} produtoId={item?.id} />)}
                </TabPanel>
                <TabPanel value={valorAba} index={1}>
                    {<SelecionarBebida adicionarItem={adicionarBebida} />}
                </TabPanel>
            </>}
        </>
    )

}



export default ControleEtapas