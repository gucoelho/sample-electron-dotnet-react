import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Typography from '@material-ui/core/Typography';
import {formatarValor} from '../../utils'
import { LinearProgress, List, ListItemText, ListItem, Divider, Paper} from '@material-ui/core';
import styled from 'styled-components'
import Item from './Item'
import ControleEtapas from './adicionar-item/ControleEtapas'

const ResumoPedido = ({ itens } : any) => (
  <Paper>
   <List>
    <Typography variant="h6">Resumo do pedido</Typography>
       {itens.length > 0 && itens.map((i : Item) => 
        (<ListItemText primary={`${i.nome} - ${i.valor}`} />) 
        )}
        <ListItemText primary={`Total:`} />
   </List> 
  </Paper>
)


const Container = styled.div`
  display: flex;
`

const Etapas = styled.div`
  flex-grow: 1;
`

const PaginaNovoPedido = () => {
    const [itens, setItens] = useState<Item[]>([]);

    const adicionaItem = (i: Item) => setItens([...itens, i])

    return <Layout pagename="Novo Pedido">
      <Container>
        <Etapas>
          <ControleEtapas />
        </Etapas>
      </Container>
    </Layout>
}


export default PaginaNovoPedido