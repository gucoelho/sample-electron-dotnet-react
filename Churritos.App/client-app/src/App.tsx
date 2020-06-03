
import React from 'react'
import Drawer from '@material-ui/core/Drawer'
import CssBaseline from '@material-ui/core/CssBaseline'
import List from '@material-ui/core/List'
import Divider from '@material-ui/core/Divider'
import ListItem from '@material-ui/core/ListItem'
import ListItemIcon from '@material-ui/core/ListItemIcon'
import ListItemText from '@material-ui/core/ListItemText'
import ViewListIcon from '@material-ui/icons/ViewList'
import ShoppingIcon from '@material-ui/icons/ShoppingBasket'
import InvertColors from '@material-ui/icons/InvertColors'
import { StylesProvider } from '@material-ui/core/styles'
import Category from '@material-ui/icons/Category'
import BrokenImageIcon from '@material-ui/icons/BrokenImage'
import styled from 'styled-components'
import { Link, Switch, Route, BrowserRouter } from 'react-router-dom'
import ChurritosImg from './assets/images/Churritos.png'
import PaginaPedido from './paginas/pedido/PaginaPedidos'
import PaginaNovoPedido from './paginas/pedido/PaginaNovoPedido'
import PaginaAdicionais from './paginas/PaginaAdicionais'
import PaginaCategorias from './paginas/PaginaCategorias'
import PaginaProdutos from './paginas/PaginaProdutos'


const drawerWidth = 240

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
  height: 128px;
  display: flex;
  justify-content: center;
`

const Logo = styled.img`
  height: 100%;
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
                        <ToolbarGap>
                            <Logo src={ChurritosImg} /> 
                        </ToolbarGap>
                        <Divider />
                        <List>
                            <ListItem button component={Link} to="/pedidos" key="pedidos">
                                <ListItemIcon>
                                    <ShoppingIcon />
                                </ListItemIcon>
                                <ListItemText primary="Pedidos" />
                            </ListItem>
                            <ListItem button component={Link} to="/produtos" key="produtos">
                                <ListItemIcon>
                                    <ViewListIcon />
                                </ListItemIcon>
                                <ListItemText primary="Produtos" />
                            </ListItem>
                            <ListItem button component={Link} to="/categorias" key="categorias">
                                <ListItemIcon>
                                    <Category />
                                </ListItemIcon>
                                <ListItemText primary="Categorias" />
                            </ListItem>
                            <ListItem button component={Link} to="/adicionais" key="adicionais">
                                <ListItemIcon>
                                    <BrokenImageIcon />
                                </ListItemIcon>
                                <ListItemText primary="Adicionais" />
                            </ListItem>
                        </List>
                        <Divider />
                    </AppDrawer>
                    <Switch>
                        <Route exact path="/pedidos" component={PaginaPedido} />
                        <Route exact path="/pedidos/criar" component={PaginaNovoPedido} />
                        <Route exact path="/categorias" component={PaginaCategorias} />
                        <Route exact path="/adicionais" component={PaginaAdicionais} />
                        <Route exact path="/produtos" component={PaginaProdutos} />
                        <Route path="/" component={PaginaPedido} />
                    </Switch>
                </BrowserRouter>
            </Container>
        </StylesProvider>
    )
}


export default App
