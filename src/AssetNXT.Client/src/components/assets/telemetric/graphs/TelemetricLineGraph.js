import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';
import { Line } from "react-chartjs-2";

import './TelemetricLineGraph.css';

export default class TelemetricLineGraph extends Component {

  getLabels(stations) {
    return stations.map(station => {
      return station.time;
    });
  }

  render() {

    const options = {
      responsive: true,
      maintainAspectRatio: true,
      scales: {
         xAxes: [{
          type: 'time',
          ticks: {
            autoskip: true,
            autoskipPadding: 30,
            maxTicksLimit: 35
          }
        }]
      }
    }

    const dataset = {
      fill: false,
      label: `${this.props.telemetricId}`,
      backgroundColor: 'rgba(75,192,192,0.4)',
      borderColor: 'rgba(75,192,192,1)',
      borderCapStyle: 'butt',
      borderDash: [],
      borderDashOffset: 0.0,
      borderJoinStyle: 'miter',
      pointBorderColor: 'rgba(75,192,192,1)',
      pointBackgroundColor: '#fff',
      pointBorderWidth: 1,
      pointHoverRadius: 5,
      pointHoverBackgroundColor: 'rgba(75,192,192,1)',
      pointHoverBorderColor: 'rgba(220,220,220,1)',
      pointHoverBorderWidth: 2,
      pointRadius: 1,
      pointHitRadius: 10,
      data: this.props.telemetricData,
    };

    const chartData = {
      datasets: [dataset],
      labels: this.getLabels(this.props.stations)
    }

    return <Line data={chartData} options={options}/>
  }
}
