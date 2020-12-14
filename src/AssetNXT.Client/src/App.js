import React, { Component } from 'react';
import { Switch, Route, Router } from 'react-router-dom';

import Home from './pages/Home';
import StationPage from './pages/StationPage';
import GeometricPage from './pages/GeometricPage';
import ConstraintPage from './pages/ConstraintPage';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <Switch>
            <Route path='/' exact component={Home} />
            <Route path='/geo' exact component={GeometricPage} />
            <Route path='/constraints' exact component={ConstraintPage} />
            <Route path='/station/:deviceId' exact component={StationPage} />
        </Switch>
    );
  }
}