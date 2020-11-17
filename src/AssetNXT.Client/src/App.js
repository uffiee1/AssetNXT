import React, { Component } from 'react';
import { Switch, Route, Router } from 'react-router-dom';

import Home from './pages/Home';
import StationPage from './pages/StationPage';
import ConstrainPage from './pages/ConstrainPage';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <Switch>
            <Route path='/' exact component={Home} />
            <Route path='/constrains' exact component={ConstrainPage} />
            <Route path='/:deviceId' exact component={StationPage} />
        </Switch>
    );
  }
}