import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import Button from '@material-ui/core/Button';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Typography from '@material-ui/core/Typography';
import { formatarValor } from '../../utils'
import { List, ListItemText, Paper } from '@material-ui/core';
import styled from 'styled-components'
import Item from './Item'
import ControleEtapas from './adicionar-item/ControleEtapas'

const ResumoPedido = ({ itens }: any) => (
  <Paper>
    <List>
      <Typography variant="h6">Resumo do pedido</Typography>
      {itens.length > 0 && itens.map((i: Item) =>
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

interface ItemPedido {
  produto: Item,
  recheio: Item,
  cobertura: Item,
}


// TODO: Ajustar fluxo de adição de novos item no pedido
const PaginaNovoPedido = ({history} : any) => {
  const [itens, setItens] = useState<ItemPedido[]>([]);
  const [adicionandoItem, setAdicionandoItem] = useState<Boolean>(false);

  const adicionaItem = (i: ItemPedido) => { setItens([...itens, i]); setAdicionandoItem(false); }

  const finalizarPedido = async () => {
    const rawResponse = await fetch('/api/pedido', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(itens.map(x => ({
        idProduto: x.produto.id,
        idRecheio: x.recheio.id,
        idCobertura: x.cobertura.id
      })))
    });

    if(rawResponse.status === 200){
        history.push('/pedidos')
    }
  }

  //TODO: Lista de resumo com tabela
  return <Layout pagename="Novo Pedido">
    <Container>
      {!adicionandoItem &&
        <>
          <Button onClick={() => setAdicionandoItem(true)}>Adicionar item</Button>
          {itens && itens.map(x => <div>
            {x.produto.nome} | Cobertura: {x.cobertura.nome} | Recheio: {x.recheio.nome} | Valor {x.produto.valor}
          </div>)}
          {itens && "Total:" + formatarValor(itens.map(x => x.produto.valor).reduce((acc, v) => acc + v, 0))}

        </>}
      {adicionandoItem &&

        <Etapas>
          <ControleEtapas adicionarItemPedido={adicionaItem} />
        </Etapas>
      }

      {itens.length > 0 &&
        <Button onClick={finalizarPedido}>Finalizar Pedido</Button>
      }

    </Container>
  </Layout>
}


export default PaginaNovoPedido