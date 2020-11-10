import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import registerServiceWorker from './registerServiceWorker';

import { Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import DevicePage from './pages/DevicePage'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
      <Switch>
        <Route path='/' exact component={Home}/>
        <Route path='/:deviceId' component={DevicePage}/>
      </Switch>
  </BrowserRouter>,
  rootElement);

registerServiceWorker();

