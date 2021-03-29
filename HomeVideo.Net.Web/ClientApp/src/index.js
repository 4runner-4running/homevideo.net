//import 'bootstrap/dist/css/bootstrap.css';
import './styles.less';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import {
    Container,
    Grid,
    Box,
    AppBar,
    Toolbar,
    CssBaseline,
    useTheme,
    createMuiTheme,
    ThemeProvider
} from '@material-ui/core';


import App from './App';
import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');


ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <App/>
    </BrowserRouter>,
  rootElement);

registerServiceWorker();

