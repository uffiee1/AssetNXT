import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import StationPage from './pages/StationPage';
import GeometricPage from './pages/GeometricPage';
import ConstraintPage from './pages/ConstraintPage';
import NotificationPage from './pages/NotificationPage';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Switch>
        <Route path='/' exact component={Home} />
        <Route path='/geo' exact component={GeometricPage} />
        <Route path='/notifications' exact component={NotificationPage} />
        <Route path='/constraints' exact component={ConstraintPage} />
        <Route path='/station/:deviceId' exact component={StationPage} />
      </Switch>
    );
  }
}