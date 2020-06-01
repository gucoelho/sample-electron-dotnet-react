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

import Box from '@material-ui/core/Box';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';




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
const PaginaNovoPedido = ({ history }: any) => {
  const [itens, setItens] = useState<ItemPedido[]>([]);
  const [adicionandoItem, setAdicionandoItem] = useState<Boolean>(false);

  const adicionaItem = (item: ItemPedido) => { 
    if(item) 
      setItens([...itens, item]); 
      
     setAdicionandoItem(false); 
  }

  const finalizarPedido = async () => {
    const rawResponse = await fetch('/api/pedido', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(itens.map(x => ({
        idProduto: x.produto.id,
        idRecheio: x.recheio?.id,
        idCobertura: x.cobertura?.id
      })))
    });

    if (rawResponse.status === 200) {
      history.push('/pedidos')
    }
  }

  const calculaValorItem = (valorProduto: number, valorRecheio : number, valorCobertura: number) =>
   (valorProduto ? valorProduto : 0) + (valorRecheio ? valorRecheio : 0) + (valorCobertura ? valorCobertura : 0) 
  
  //TODO: Lista de resumo com tabela
  return <Layout pagename="Novo Pedido">
    {!adicionandoItem &&
      <>
        <Button onClick={() => setAdicionandoItem(true)}>Adicionar Item</Button>
        <TableContainer component={Paper}>
          <Table aria-label="collapsible table">
            <TableHead>
              <TableRow>
                <TableCell />
                <TableCell>Produto</TableCell>
                <TableCell/>
                <TableCell/>
                <TableCell>Valor</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {itens && itens.map(x => <TableRow key={x.produto.id}>
                <TableCell/>
                <TableCell>
                  {x.produto.nome}
                </TableCell>
                  <TableCell>
                    {x.recheio?.nome}
                  </TableCell>
                <TableCell>
                  {x.cobertura?.nome}
                </TableCell>
                <TableCell>{formatarValor(calculaValorItem(x.produto.valor, x.recheio?.valor, x.cobertura?.valor))}</TableCell>
              </TableRow>)}
              <TableRow>
                <TableCell />
                <TableCell>
                  <Typography variant="subtitle1">
                    Total
                  </Typography>
                </TableCell>
                <TableCell/>
                <TableCell/>
                <TableCell>
                  <Typography variant="h6">
                  {itens && formatarValor(itens.map(x => calculaValorItem(x.produto.valor, x.recheio?.valor, x.cobertura?.valor)).reduce((acc, v) => acc + v, 0))}
                  </Typography>
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
      </>}

    <Container>
      {adicionandoItem &&

        <Etapas>
          <ControleEtapas adicionarItemPedido={adicionaItem} />
        </Etapas>
      }

      {itens.length > 0 && !adicionandoItem &&
        <Button onClick={finalizarPedido}>Finalizar Pedido</Button>
      }
    </Container>
  </Layout>
}


export default PaginaNovoPedido