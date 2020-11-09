import React, { Component } from 'react';
import { Layout } from './components/Layout';

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = { assets: [], loading: true }
  }

  componentDidMount() {
    this.fetchStationData();
  }

  render () {
    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <Layout assets={this.state.assets} />

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
