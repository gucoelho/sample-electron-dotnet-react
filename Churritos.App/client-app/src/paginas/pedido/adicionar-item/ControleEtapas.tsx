import React, { useState, useEffect } from 'react'
import Button from '@material-ui/core/Button';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Typography from '@material-ui/core/Typography';
import Item from '../Item'
import SelecionarProduto from './SelecionarProduto'
import SelecionarBebida from './SelecionarBebida'
import SelecionarCobertura from './SelecionarCobertura'
import SelecionarRecheio from './SelecionarRecheio'
import styled from 'styled-components'
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Box from '@material-ui/core/Box';

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
);

function getSteps() {
  return ['Selecionar produto', 'Selecionar recheio', 'Selecionar cobertura', 'Selecionar adicional'];
}

function getStepContent(step: number) {
  switch (step) {
    case 0:
      return 'Selecione o produto:';
    case 1:
      return 'Selecione o recheio:';
    case 2:
      return 'Selecione a cobertura:';
    case 3:
      return 'Selecione adicional:';
    default:
      return 'Unknown step';
  }
}


function HorizontalLinearStepper({ activeStep, setActiveStep, adicionarItem, voltar } : any) {
  const classes = useStyles();
  const [skipped, setSkipped] = React.useState(new Set<number>());
  const steps = getSteps();

  const isStepOptional = (step: number) => {
    return step > 0;
  };

  const isStepSkipped = (step: number) => {
    return skipped.has(step);
  };

  const handleBack = () => {
    if(activeStep === 0)
      voltar()

    setActiveStep((prevActiveStep : number) => prevActiveStep - 1);
  };

  const handleSkip = () => {
    if (!isStepOptional(activeStep)) {
      throw new Error("You can't skip a step that isn't optional.");
    }

    setActiveStep((prevActiveStep : number) => prevActiveStep + 1);
    setSkipped((prevSkipped) => {
      const newSkipped = new Set(prevSkipped.values());
      newSkipped.add(activeStep);
      return newSkipped;
    });
  };

  const handleReset = () => {
    setActiveStep(0);
  };

  return (
    <div className={classes.root}>
      <Stepper activeStep={activeStep}>
        {steps.map((label, index) => {
          const stepProps: { completed?: boolean } = {};
          const labelProps: { optional?: React.ReactNode } = {};
          if (isStepOptional(index)) {
            labelProps.optional = <Typography variant="caption">Optional</Typography>;
          }
          if (isStepSkipped(index)) {
            stepProps.completed = false;
          }
          return (
            <Step key={label} {...stepProps}>
              <StepLabel {...labelProps}>{label}</StepLabel>
            </Step>
          );
        })}
      </Stepper>
      <div>
        {activeStep === steps.length ? (
          <div>
            <Typography className={classes.instructions}>
              All steps completed - you&apos;re finished
            </Typography>
            <Button onClick={handleReset} className={classes.button}>
              Reset
            </Button>
          </div>
        ) : (
          <div>
            <Typography className={classes.instructions}>{getStepContent(activeStep)}</Typography>
            <div>
              <Button onClick={handleBack} className={classes.button}>
                Voltar
              </Button>

              <Button disabled={activeStep !== 3} onClick={adicionarItem} className={classes.button}>
                Adicionar
              </Button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

interface TabPanelProps {
  children?: React.ReactNode;
  index: any;
  value: any;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

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
  );
}

interface Cobertura {
    id: number,
    nome: string
}

interface Recheio {
    id: number,
    nome: string
}


interface Categoria {
    id: number,
    nome: string
}


const ControleEtapas = ({adicionarItemPedido} : any) => {
    const [etapa, setEtapa] = useState(0);
    const [item, setItem] = useState<Item>()
    const [cobertura, setCobertura] = useState<Cobertura>()
    const [recheio, setRecheio] = useState<Recheio>()
    const [valorAba, setValorAba] = useState(0);
    const [categorias, setCategorias] = useState<Categoria[]>();
    const [loading, setLoading] = useState(false);


    useEffect(() => {
      fetch('/api/categoria')
            .then(res => res.json())
            .then(data => setCategorias(data))
            .then(() => setValorAba(0))
            .then(() => setLoading(false))
    }, [])

    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
      setItem(undefined)
      setCobertura(undefined)
      setRecheio(undefined)
      setValorAba(newValue);
    }


    const adicionarItem = (i: Item) => { 
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
        setEtapa((prevActiveStep : number) => prevActiveStep + 1);
    }

    const finalizarItem = () => {
        adicionarItemPedido({
          produto: item,
          cobertura: cobertura,
          recheio: recheio
        })
    }

    const adicionarBebida = (bebida : Item) => {
        adicionarItemPedido({produto: bebida})
    }


    return (
    <>
    <AppBar position="static">
        <Tabs value={valorAba} onChange={handleChange} aria-label="simple tabs example">
          {categorias && categorias.map((c : Categoria) => <Tab key={`tab-${c.nome}`} label={c.nome} />)}
       </Tabs>
      </AppBar>
      <TabPanel value={valorAba} index={0}>
        <HorizontalLinearStepper 
        activeStep={etapa} 
        setActiveStep={setEtapa} 
        adicionarItem={finalizarItem}
        voltar={() => adicionarItemPedido(undefined)} 
        />
        {(etapa === 0) && (<SelecionarProduto adicionarItem={adicionarItem} />)}
        {(etapa === 1) && (<SelecionarRecheio adicionarRecheio={adicionarRecheio} produtoId={item?.id} />)}
        {(etapa === 2) && (<SelecionarCobertura adicionarCobertura={adicionarCobertura} produtoId={item?.id} />)}
      </TabPanel>
      <TabPanel value={valorAba} index={1}>
        {<SelecionarBebida adicionarItem={adicionarBebida} />}
      </TabPanel>
    </>
    )

}

export default ControleEtapas