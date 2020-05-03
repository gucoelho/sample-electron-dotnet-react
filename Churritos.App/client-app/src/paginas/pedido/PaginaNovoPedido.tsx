import React, { useState, useEffect, useDebugValue } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Typography from '@material-ui/core/Typography';
import {formatarValor} from '../../utils'
import { LinearProgress, List, ListItemText, ListItem, Divider} from '@material-ui/core';

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
  return ['Selecionar categoria', 'Selecionar recheio', 'Selecionar cobertura', 'Selecionar adicional'];
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
              {isStepOptional(activeStep) && (
                <Button
                  variant="contained"
                  color="primary"
                  onClick={handleSkip}
                  className={classes.button}
                >
                  Skip
                </Button>
              )}
              <Button
                variant="contained"
                color="primary"
                onClick={handleNext}
                className={classes.button}
              >
                {activeStep === steps.length - 1 ? 'Finish' : 'Next'}
              </Button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

interface Item {
    id: number,
    nome: string,
    valor: number
}

const SelecionarProduto = ({setPedido : any} : any) => {
    const [produtos, setProdutos] = useState([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
         setLoading(true)
         fetch("/api/produto")
            .then(res => res.json())
            .then(data => data.map((item: Item) => ({...item, valor: formatarValor(item.valor)})))
            .then(data => setProdutos(data))
            .then(() => setLoading(false))
    }, []);

   return <div>
     {loading && <LinearProgress />}
     {!loading && produtos.map((p :Item) => {
         return <List>
             <Divider />
             <ListItem button divider>
                 <ListItemText primary={`${p.nome} - ${p.valor}`} />
             </ListItem>
         </List>
     })}
   </div> 
}

const ResumoPedido = ({ pedido } : any) => (
   <List>
    <Typography variant="h4">Resumo do pedido</Typography>
       {pedido.itens.map((i : Item) => 
        (<ListItemText primary={`${i.nome} - ${i.valor}`} />) 
        )}
        <ListItemText primary={`Total:`} />
   </List> 
)

const PaginaNovoPedido = () => {
    const [pedido, setPedido] = useState({ itens: []});
    const [etapa, setEtapa] = useState(0);

    return <Layout pagename="Novo Pedido">
        <HorizontalLinearStepper activeStep={etapa} setActiveStep={setEtapa}/>
        {(etapa === 0) && (<SelecionarProduto  />)}
        <ResumoPedido pedido={pedido} />
    </Layout>
}


export default PaginaNovoPedido