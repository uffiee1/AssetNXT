import React, { Component } from "react";

import './StationPage.css'
import Layout from '../components/Layout';

import TelemetricView from "../components/assets/telemetric/TelemetricView";
import TelemetricDashboard from "../components/assets/telemetric/TelemetricDashboard";
import TelemetricDataTemplate from "../components/assets/telemetric/TelemetricDataTemplate";

import agent from "../api/agent";

export default class StationPage extends Component {
  static displayName = StationPage.displayName

  state = {
    loadingRoutes: true,
    loadingStations: true,
    loadingConstraints: true
  }

  componentDidMount() {
    this.fetchRoutesData();
    this.fetchStationData();
    this.fetchConstraintData();
  }

  renderComponent(stations, routes, constraints) {

    const telemetricView =
      <TelemetricView
        routes={routes}
        stations={stations}
        constraints={constraints}
        componentDataSnapshot={TelemetricDashboard}
        componentDataTemplate={TelemetricDataTemplate} />

    return <Layout dock={telemetricView} />
  }

  render() {

    var contents =
      this.state.loadingRoutes ||
        this.state.loadingStations ||
        this.state.loadingConstraints

        ? <Layout dock={<p><em>Loading...</em></p>} />
        : this.renderComponent(this.state.stations, this.state.routes, this.state.constraints);

    return contents;
  }

  async fetchStationData() {
    const request = `${agent.baseUrl}/api/stations/all/${this.props.match.params.deviceId}`;

    const response = await fetch(request);
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loadingStations: false, stations: data });
  }

  async fetchRoutesData() {
    //const request = `${agent.baseUrl}/api/routes/device/${this.props.match.params.deviceId}`;

    //const response = await fetch(request);
    const response = await agent.Routes.getRoutesByDeviceId(this.props.match.params.deviceId);
    console.log("Response:");
    console.log(response);

    //const data = await response.json();
    const data = await response;
    console.log("Data:");
    console.log(data);

    this.setState({ loadingRoutes: false, routes: data });
  }

  async fetchConstraintData() {
    //const request = `api/constraints/device/${this.props.match.params.deviceId}`;

    //const response = await fetch(request);
    const response = await agent.Telemetric.getConstrainsByDeviceId(this.props.match.params.deviceId);
    console.log("Response:");
    console.log(response);

    //const data = await response.json();
    const data = await response;
    console.log("Data:");
    console.log(data);

    this.setState({ loadingConstraints: false, constraints: data });
  }
}