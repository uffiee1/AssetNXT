import React, { Component } from "react";

import './AssetTooltip.css'
import Asset from "../Asset";
import Tooltip from '../../map/Tooltip';

export default class AssetTooltip extends Component {

  state = {
    loading: false
  }

  componentDidMount() {
  }

  componentDidUpdate() {
  }

  getTemperatureLabel() {
     return <label>Temperature: </label>;
  }

  getTemperature(value) {
    return <label>{Math.round(value)}&deg;C</label>
  }

  getPressureLabel() {
     return <label>Pressure: </label>
  }

  getPressure(value) {
        return <label>{Math.round(value)} Pa</label>
  }

  getHumidityLabel() {
    return <label>Humidity: </label>
  }

  getHumidity(value) {
    return <label>{Math.round(value)}%</label>
  }

  renderComponent(asset, tag) {

    const labels = [
      this.getTemperatureLabel(),
      this.getHumidityLabel(),
      this.getPressureLabel()
    ]
    
    const values = [
      this.getTemperature(tag.temperature),
      this.getHumidity(tag.humidity),
      this.getPressure(tag.pressure)
    ]

    var link = `/${asset.deviceId}/`;

    return (
      <Tooltip link={link}
        values={values}
        labels={labels}>

        <Asset title={tag.id}
          description={tag.id}>
        </Asset>

      </Tooltip>
    );

  }

  render() {

    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderComponent(this.props.asset, this.props.tag);

    return contents;
  }
}