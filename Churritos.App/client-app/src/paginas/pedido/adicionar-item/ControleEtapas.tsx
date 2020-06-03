import React, { useState, useEffect } from 'react'
import Button from '@material-ui/core/Button'
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles'
import Stepper from '@material-ui/core/Stepper'
import Step from '@material-ui/core/Step'
import StepLabel from '@material-ui/core/StepLabel'
import Typography from '@material-ui/core/Typography'
import { Produto, ItemPedido, Adicional } from '../Models'
import SelecionarProduto from './SelecionarProduto'
import SelecionarBebida from './SelecionarBebida'
import SelecionarCobertura from './SelecionarCobertura'
import SelecionarRecheio from './SelecionarRecheio'
import SelecionarExtras from './SelecionarExtras'
import AppBar from '@material-ui/core/AppBar'
import Tabs from '@material-ui/core/Tabs'
import Tab from '@material-ui/core/Tab'
import Box from '@material-ui/core/Box'

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: '100%',
        },
        button: {
            marginRight: theme.spacing(1),
        },
        instructions: {
            marginTop: theme.spacing(1),
            marginBottom: theme.spacing(1),
        },
    }),
)

function getSteps() {
    return ['Selecionar produto', 'Selecionar recheio', 'Selecionar cobertura', 'Selecionar adicional']
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


function HorizontalLinearStepper({ activeStep, setActiveStep }: any) {
    const steps = getSteps()

    const isStepOptional = (step: number) => {
        return step > 0
    }

    const handleBack = () => {
        setActiveStep((prevActiveStep: number) => prevActiveStep - 1)
    }

    const handleReset = () => {
        setActiveStep(0)
    }

    return (
        <div>
            <Stepper activeStep={activeStep}>
                {steps.map((label, index) => {
                    const stepProps: { completed?: boolean } = {}
                    const labelProps: { optional?: React.ReactNode } = {}
                    if (isStepOptional(index)) {
                        labelProps.optional = <Typography variant="caption">Optional</Typography>
                    }
                    return (
                        <Step key={label} {...stepProps}>
                            <StepLabel {...labelProps}>{label}</StepLabel>
                        </Step>
                    )
                })}
            </Stepper>
            <div>
                {activeStep !== steps.length && (<div>
                    <Typography>{getStepContent(activeStep)}</Typography>
                    <div>
                        <Button onClick={handleBack}>Voltar</Button>
                    </div>
                </div>)}
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

interface ControleEtapasProps {
    adicionarItemPedido: (item: ItemPedido) => void
}

const ControleEtapas = ({ adicionarItemPedido }: any) => {
    const [etapa, setEtapa] = useState(0)
    const [item, setItem] = useState<Produto>()
    const [cobertura, setCobertura] = useState<Adicional>()
    const [recheio, setRecheio] = useState<Adicional>()
    const [valorAba, setValorAba] = useState(0)
    const [categorias, setCategorias] = useState<Categoria[]>()
    const [extras, setExtras] = useState<Adicional[]>()
    const [loading, setLoading] = useState(false)


    useEffect(() => {
        fetch('/api/categoria')
            .then(res => res.json())
            .then(data => setCategorias(data))
            .then(() => setValorAba(0))
            .then(() => setLoading(false))
    }, [])

    const handleChange = (event: React.ChangeEvent<unknown>, newValue: number) => {
        setItem(undefined)
        setCobertura(undefined)
        setRecheio(undefined)
        setExtras([])
        setValorAba(newValue)
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


    const proximaEtapa = () => {
        setEtapa((prevActiveStep: number) => prevActiveStep + 1)
    }

    const finalizarItem = () => {
        const adicionais: Adicional[] = []

        if (cobertura)
            adicionais.push(cobertura)

        if (recheio)
            adicionais.push(recheio)

        if (extras && extras?.length > 0)
            adicionais.push(...extras)

        adicionarItemPedido({ produto: item!, adicionais })
    }

    const adicionarBebida = (bebida: Produto) => {
        adicionarItemPedido({ produto: bebida })
    }

    const adicionarExtras = (extras: Adicional[]) => {
        setExtras(extras)
        proximaEtapa()
    }


    return (
        <>
            <AppBar position="static">
                <Tabs value={valorAba} onChange={handleChange} aria-label="simple tabs example">
                    {categorias && categorias.map((c: Categoria) => <Tab key={`tab-${c.nome}`} label={c.nome} />)}
                </Tabs>
            </AppBar>
            <TabPanel value={valorAba} index={0}>
                <HorizontalLinearStepper
                    activeStep={etapa}
                    setActiveStep={setEtapa}
                    adicionarItem={finalizarItem}
                />
                {(etapa === 0) && (<SelecionarProduto adicionarItem={adicionarItem} />)}
                {(etapa === 1) && (<SelecionarRecheio adicionarRecheio={adicionarRecheio} produtoId={item?.id} />)}
                {(etapa === 2) && (<SelecionarCobertura adicionarCobertura={adicionarCobertura} produtoId={item?.id} />)}
                {(etapa === 3) && (<SelecionarExtras adicionarExtras={adicionarExtras} produtoId={item?.id} />)}
                {(etapa === 4) && (<Button onClick={finalizarItem}> Adicionar Item </Button>)}
            </TabPanel>
            <TabPanel value={valorAba} index={1}>
                {<SelecionarBebida adicionarItem={adicionarBebida} />}
            </TabPanel>
        </>
    )

}

export default ControleEtapas