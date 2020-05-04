import React, { useState } from 'react'
import Button from '@material-ui/core/Button';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Typography from '@material-ui/core/Typography';
import Item from '../Item'
import SelecionarProduto from './SelecionarProduto'
import SelecionarCobertura from './SelecionarCobertura'
import SelecionarRecheio from './SelecionarRecheio'
import styled from 'styled-components'

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


function HorizontalLinearStepper({ activeStep, setActiveStep } : any) {
  const classes = useStyles();
  const [skipped, setSkipped] = React.useState(new Set<number>());
  const steps = getSteps();

  const isStepOptional = (step: number) => {
    return step > 0;
  };

  const isStepSkipped = (step: number) => {
    return skipped.has(step);
  };

  const handleNext = () => {
    let newSkipped = skipped;
    if (isStepSkipped(activeStep)) {
      newSkipped = new Set(newSkipped.values());
      newSkipped.delete(activeStep);
    }

    setActiveStep((prevActiveStep : number) => prevActiveStep + 1);
    setSkipped(newSkipped);
  };

  const handleBack = () => {
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
              <Button disabled={activeStep === 0} onClick={handleBack} className={classes.button}>
                Back
              </Button>
            </div>
          </div>
        )}
      </div>
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


const ControleEtapas = () => {
    const [etapa, setEtapa] = useState(0);
    const [item, setItem] = useState<Item>()
    const [cobertura, setCobertura] = useState<Cobertura>()
    const [recheio, setRecheio] = useState<Recheio>()

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


    return (
    <>
        <HorizontalLinearStepper activeStep={etapa} setActiveStep={setEtapa}/>
        <div>{item?.nome}</div>
        <div>{item?.categoriaId}</div>
        <div>{cobertura?.nome}</div>
        <div>{recheio?.nome}</div>
        {(etapa === 0) && (<SelecionarProduto adicionarItem={adicionarItem} />)}
        {(etapa === 1) && (<SelecionarRecheio adicionarRecheio={adicionarRecheio} categoriaId={item?.categoriaId} />)}
        {(etapa === 2) && (<SelecionarCobertura adicionarCobertura={adicionarCobertura} />)}
    </>
    )

}

export default ControleEtapas