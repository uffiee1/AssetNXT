import React, { Component } from 'react';

import Layout from '../components/Layout';
import RouteConfig from '../components/constraints/geometrics/RouteConfig';
import agent from "../api/agent";

export default class GeometricPage extends Component {
  static displayName = GeometricPage.displayName

  state = {
    routes: [],
    loading: true,
  }

  componentDidMount() {
    this.fetchRouteData();
  }

  renderComponent(routes) {

    var routeConfig =
      <RouteConfig routes={routes}
        stateHasChanged={() => this.fetchRouteData()} />

    return <Layout dock={routeConfig} />
  }

  render() {
    var contents = this.state.loading
      ? <Layout dock={<p><em>Loading...</em></p>} />
      : this.renderComponent(this.state.routes);

    return contents;
  }

  async fetchRouteData() {
    const response = await agent.Geometric.routes();
    this.setState({ loading: false, routes: response });
  }
}
