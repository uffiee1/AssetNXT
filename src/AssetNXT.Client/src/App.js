import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';

import Home from './pages/Home';
import DevicePage from './pages/DevicePage'


export default class App extends Component {
  static displayName = App.name;

  render() {
    return(
      <Switch>
        <Route path='/' exact component={Home}/>
        <Route path='/:deviceId' component={DevicePage}/>
      </Switch>
    );
  }
}