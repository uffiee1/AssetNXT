import React, { Component } from "react";

import './StationPage.css'
import Layout from '../components/Layout';
import TelemetricTabs from "../components/assets/telemetric/TelemetricTabs";
import TelemetricDataTemplate from "../components/assets/telemetric/TelemetricDataTemplate";

export default class StationPage extends Component {
  static displayName = StationPage.displayName

  state = {
    loading: true
  }

  componentDidMount() {
    this.fetchStationData();
  }

  renderComponent(stations) {

    const telemetricTabs =
      <TelemetricTabs stations={stations}
        telemetricTemplate={TelemetricDataTemplate} />

    return <Layout dock={telemetricTabs} />
  }

  render() {

    var contents = this.state.loading
      ? <Layout dock={<p><em>Loading...</em></p>} />
      : this.renderComponent(this.state.stations);

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

    this.setState({ loading: false, stations: data });
  }
}