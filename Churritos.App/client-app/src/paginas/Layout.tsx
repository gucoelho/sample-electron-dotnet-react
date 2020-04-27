
import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import styled from 'styled-components';

const drawerWidth = 240;

const ApplicationBar = styled(AppBar)`
      width: ${`calc(100% - ${drawerWidth}px)`};
      margin-left: ${drawerWidth}px;
`
const Content = styled.section`
  margin-top: 64px;
  flex-grow: 1;
  padding: 2rem;
`

interface ILayoutPropType {
    pagename: string,
    children: any
}

const Layout = ({ pagename, children } : ILayoutPropType) => <>
    <ApplicationBar position="fixed">
        <Toolbar>
            <Typography variant="h6" noWrap>
                {pagename}
            </Typography>
        </Toolbar>
    </ApplicationBar>
    <Content>
        {children}
    </Content>
</>

export default Layout