import React, { Component } from 'react';
import { Route } from 'react-router';

import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { LineChart } from './components/graphs/LineChart';

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = { assets: [], loading: true }
  }

  componentDidMount() {
    this.fetchStationData();
  }

  render() {

    var children = ([

       <Route exact path='/' 
              render={(routeProps) => (
                <Home {...routeProps} 
                assets={this.state.assets} /> )} />,

       <Route path='/details/:deviceId'
              component={LineChart} />
    ]);


    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <Layout children={children}/>

    return(contents);
  }

  async fetchStationData() {
    const request = 'api/stations';

    const response = await fetch(request);
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loading: false, assets: data });
  }
}