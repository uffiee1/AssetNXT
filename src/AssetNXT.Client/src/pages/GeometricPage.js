import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import Layout from '../components/Layout';
import RouteConfig from '../components/routes/RouteConfig';
import GeometricMap from '../components/geoconfig/GeometricMap';
import GeometricModal from '../components/geoconfig/GeometricModal';

export default class GeometricPage extends Component
{
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
        stateHasChanged={() => this.fetchRouteData()}/>

    return <Layout dock={routeConfig}/>
  }

  render() {
     var contents = this.state.loading
      ? <Layout dock={<p><em>Loading...</em></p>}/>
      : this.renderComponent(this.state.routes);

    return contents;
  }
  
  async fetchRouteData() {

    const request = `api/routes`;

    const response = await fetch(request);
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loading: false, routes: data });
  }
}
