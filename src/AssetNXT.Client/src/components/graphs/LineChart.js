import React, { Component } from "react";
import { Container } from "reactstrap";
import { Line } from 'react-chartjs-2'; 

import './LineChart.css';



export class LineChart extends Component {

  state = {
    assets: [],
    loading: true,
    lineData: null,
    lineOptions: null
  }

  componentDidMount() {
    this.fetchStationData();
  }

  render() {

     var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <Line data={this.state.lineData} options={this.state.lineOptions} />

    return(contents);
  }

  async fetchStationData(){

    const request = 'api/stations';
    const response = await fetch(request);
    const data = await response.json();

    this.setState({ assets: data });
    await this.mapStationData(this.state.assets);
  }

  async mapStationData(assets) {

     var labels = assets.map((asset) => {
       return asset.tags[0].createDate; 
     });

      
    console.log(labels);
    var data = assets.map((asset) => {
      return asset.tags[0].temperature
    });

    const lineOptions = {
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

    const lineData = { 
      labels: labels,
      datasets: [
      {
      label: "My First Dataset",
      fill: false,
      lineTension: 0.1,
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
        data: data
      }]
    }

    this.setState({ loading: false, lineData: lineData, lineOptions: lineOptions });
  }
}
