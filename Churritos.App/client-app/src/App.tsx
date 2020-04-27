
import React from 'react';
import Drawer from '@material-ui/core/Drawer';
import CssBaseline from '@material-ui/core/CssBaseline';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import ViewListIcon from '@material-ui/icons/ViewList';
import InvertColors from '@material-ui/icons/InvertColors';
import styled from 'styled-components';
import { StylesProvider } from '@material-ui/core/styles';
import { Link, Switch, Route, BrowserRouter } from 'react-router-dom';
import PaginaPedido from './paginas/PaginaPedidos';
import PaginaCoberturas from './paginas/PaginaCoberturas';
import PaginaRecheios from './paginas/PaginaRecheios';
import PaginaCategorias from './paginas/PaginaCategorias';

const drawerWidth = 240;

const Container = styled.div`
  display: flex;
`

const AppDrawer = styled(Drawer)`
  width: ${drawerWidth}px;
  flex-shrink: 0;

  & > .MuiDrawer-paper {
    width: ${drawerWidth}px;
  }
`

const ToolbarGap = styled.div`
  height: 64px;
`

function App() {

  return (
    <StylesProvider injectFirst>
      <Container>
        <BrowserRouter>
          <CssBaseline />
          <AppDrawer
            variant="permanent"
            anchor="left"
          >
            <ToolbarGap />
            <Divider />
            <List>
              <ListItem button component={Link} to="/pedidos" key="pedidos">
                <ListItemIcon>
                  <ViewListIcon />
                </ListItemIcon>
                <ListItemText primary="Pedidos" />
              </ListItem>
             <ListItem button component={Link} to="/categorias" key="categorias">
                <ListItemIcon>
                  <InvertColors />
                </ListItemIcon>
                <ListItemText primary="Categorias" />
              </ListItem>
              <ListItem button component={Link} to="/coberturas" key="coberturas">
                <ListItemIcon>
                  <InvertColors />
                </ListItemIcon>
                <ListItemText primary="Coberturas" />
              </ListItem>
              <ListItem button component={Link} to="/recheios" key="Categorias">
                <ListItemIcon>
                  <InvertColors />
                </ListItemIcon>
                <ListItemText primary="Recheios" />
              </ListItem>
            </List>
            <Divider />
          </AppDrawer>
          <Switch>
            <Route exact path="/pedidos" component={PaginaPedido} />
            <Route exact path="/categorias" component={PaginaCategorias} />
            <Route exact path="/recheios" component={PaginaRecheios} />
            <Route exact path="/coberturas" component={PaginaCoberturas} />
            <Route path="/" component={PaginaPedido} />
          </Switch>
        </BrowserRouter>
      </Container>
    </StylesProvider>
  );
}


export default App;
