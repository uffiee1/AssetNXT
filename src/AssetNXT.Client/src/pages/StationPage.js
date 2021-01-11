import React, { Component } from "react";

import './StationPage.css'
import Layout from '../components/Layout';

import TelemetricView from "../components/assets/telemetric/TelemetricView";
import TelemetricDashboard from "../components/assets/telemetric/TelemetricDashboard";
import TelemetricDataTemplate from "../components/assets/telemetric/TelemetricDataTemplate";


export default class StationPage extends Component {
  static displayName = StationPage.displayName

  state = {
    loadingRoutes: true,
    loadingStations: true
  }

  componentDidMount() {
    this.fetchRoutesData();
    this.fetchStationData();
  }

  renderComponent(stations, routes) {

    const telemetricView =
      <TelemetricView 
        routes={routes}
        stations={stations}
        componentDataSnapshot={TelemetricDashboard}
        componentDataTemplate={TelemetricDataTemplate} />

    return <Layout dock={telemetricView}/>
  }

  render() {

     var contents = 
       this.state.loadingRoutes || 
       this.state.loadingStations

      ? <Layout dock={<p><em>Loading...</em></p>}/>
      : this.renderComponent(this.state.stations, this.state.routes);

    return contents;
  }

  async fetchStationData() {
    const request = `api/stations/all/${this.props.match.params.deviceId}`;

    const response = await fetch(request);
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loadingStations: false, stations: data });
  }

  async fetchRoutesData() {
    const request = `api/routes/device/${this.props.match.params.deviceId}`;

    const response = await fetch(request);
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loadingRoutes: false, routes: data });
  }
}