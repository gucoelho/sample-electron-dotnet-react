
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
import { StylesProvider } from '@material-ui/core/styles'
import Category from '@material-ui/icons/Category'
import BrokenImageIcon from '@material-ui/icons/BrokenImage'
import styled from 'styled-components'
import { Link, Switch, Route, BrowserRouter } from 'react-router-dom'
import ChurritosImg from './assets/images/Churritos.png'
import PaginaPedidos from './paginas/pedido/PaginaPedidos'
import PaginaDetalhePedido from './paginas/pedido/PaginaPedido'
import PaginaNovoPedido from './paginas/pedido/PaginaNovoPedido'
import PaginaAdicionais from './paginas/PaginaAdicionais'
import PaginaCategorias from './paginas/PaginaCategorias'
import PaginaEditarProduto from './paginas/produto/PaginaEditarProduto'
import PaginaProdutos from './paginas/PaginaProdutos'
import MomentUtils from '@date-io/moment'
import { MuiPickersUtilsProvider } from '@material-ui/pickers'
import moment from 'moment'
import {
    createMuiTheme,
    ThemeProvider,
} from '@material-ui/core/styles'
import 'moment/locale/pt-br'
moment.locale('pt-br')


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

const theme = createMuiTheme({
    palette: {
        primary: {
            main: '#67c1c0',
            contrastText: '#ffffff',
        },
        secondary: {
            light: '#0066ff',
            main: '#6c3317',
            // dark: será calculada com base palette.secondary.main,
            contrastText: '#ffffff',
        },
        // Usado por `getContrastText()` para maximizar o contraste entre
        // o plano de fundo e o texto.
        contrastThreshold: 3,
        // Usado pelas funções abaixo para mudança de uma cor de luminância por aproximadamente
        // dois índices dentro de sua paleta tonal.
        // Por exemplo, mude de Red 500 para Red 300 ou Red 700.
        tonalOffset: 0.2,
    },
})


const App = () => (<StylesProvider injectFirst>
    <MuiPickersUtilsProvider utils={MomentUtils}>

        <ThemeProvider theme={theme}>
            <Container>
                <BrowserRouter>
                    <CssBaseline />
                    <AppDrawer variant="permanent" anchor="left">
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
                        <Route exact path="/pedido/:id" component={PaginaDetalhePedido} />
                        <Route exact path="/pedidos" component={PaginaPedidos} />
                        <Route exact path="/pedidos/criar" component={PaginaNovoPedido} />
                        <Route exact path="/categorias" component={PaginaCategorias} />
                        <Route exact path="/adicionais" component={PaginaAdicionais} />
                        <Route exact path="/produtos" component={PaginaProdutos} />
                        <Route exact path="/produto/:id" component={PaginaEditarProduto} />
                        <Route path="/" component={PaginaPedidos} />
                    </Switch>
                </BrowserRouter>
            </Container>

        </ThemeProvider>


    </MuiPickersUtilsProvider>
</StylesProvider>)


export default App
