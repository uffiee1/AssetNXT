import React, { Component } from "react";
import { Line } from "react-chartjs-2";
import 'chartjs-plugin-annotation';

import './TelemetricLineGraph.css';

export default class TelemetricLineGraph extends Component {

  render() {

    const {
      constraints,
      telemetricData,
      telemetricLabels,
      telemetricDataPath,
      telemetricDataName,
      telemetricDataSource } = this.props;



    const options = {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        xAxes: [{
          type: 'time',
          time: {
            tooltipFormat: 'MM/DD/YYYY h:mm:ss',
          },
          ticks: {
            autoskip: true,
            maxTicksLimit: 35,
            autoskipPadding: 30,
            min: this.props.minDate,
            max: this.props.maxDate
          }
        }]
      },
      tooltips: {
        callbacks: {
          label: function (tooltippItem) {
            return telemetricLabels[tooltippItem.index];
          }
        }
      }
    }

    const labels =
      telemetricDataSource.map(
        source => source.time);

    const data = (canvas) => {

      const ctx = canvas.getContext("2d");


      if (!!constraints && constraints.length > 0) {
        const minValue = Math.min(constraints.map(constraint => constraint[telemetricDataPath + 'Min']))
        const maxValue = Math.max(constraints.map(constraint => constraint[telemetricDataPath + 'Max']))



        const gradient = ctx.createLinearGradient(0, 0, ctx.canvas.width, 0);
        gradient.addColorStop(0.0, 'rgba(255, 0, 0, 1)');
        gradient.addColorStop(1.0, 'rgba(0, 255, 0, 1)');

        return {
          labels, datasets: [{
            fill: true,
            label: telemetricDataName,
            backgroundColor: 'rgba(75,192,192,0.3)',
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
            data: telemetricData.map(data => Math.max(data[telemetricDataPath]))
          }, {
            fill: false,
            label: `Min ${telemetricDataPath}`,
            backgroundColor: 'rgba(255,99,132,0.3)',
            borderColor: 'rgba(255,99,132,1)',
            borderCapStyle: 'butt',
            borderDash: [],
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: 'rgba(255,99,132,1)',
            pointBackgroundColor: '#fff',
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            pointHoverBackgroundColor: 'rgba(255,99,132,0.4)',
            pointHoverBorderColor: 'rgba(255,99,132,1)',
            pointHoverBorderWidth: 2,
            pointRadius: 1,
            pointHitRadius: 10,
            data: telemetricData.map(data => minValue)
          }, {
            fill: false,
            label: `Max ${telemetricDataPath}`,
            backgroundColor: 'rgba(255,99,132,0.3)',
            borderColor: 'rgba(255,99,132,1)',
            borderCapStyle: 'butt',
            borderDash: [],
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: 'rgba(255,99,132,1)',
            pointBackgroundColor: '#fff',
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            pointHoverBackgroundColor: 'rgba(255,99,132,0.4)',
            pointHoverBorderColor: 'rgba(255,99,132,1)',
            pointHoverBorderWidth: 2,
            pointRadius: 1,
            pointHitRadius: 10,
            data: telemetricData.map(data => maxValue)
          }]
        };
      } else {
        return {
          labels, datasets: [{
            fill: true,
            label: telemetricDataName,
            backgroundColor: 'rgba(75,192,192,0.3)',
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
            data: telemetricData.map(data => data[telemetricDataPath])
          }]
        };
      }
    }

    return <Line options={options} data={data} />
  }
}
