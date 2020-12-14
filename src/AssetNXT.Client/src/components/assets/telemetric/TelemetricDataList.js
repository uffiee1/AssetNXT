import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataList.css'
import './TelemetricDataListItem.css'

import TelemetricDataListItem from './TelemetricDataListItem';

export default class TelemetricDataList extends React.Component {

  render() {
    return(
      <Container className="telemetric-list-container">
        <Row className="telemetric-list-row">
          <Col className="telemetric-list-column">

            {this.props.stations.map(station => {

              console.log(`minDate: ${this.props.minDate}`);
              console.log(`maxDate: ${this.props.maxDate}`);
              console.log(`station: ${station.time}`);
              console.log(`ranged min: ${station.time > this.props.minDate}`);
              console.log(`ranged max: ${station.time < this.props.maxDate}`);

              if ((!this.props.minDate || station.time > this.props.minDate) &&
                  (!this.props.maxDate || station.time < this.props.maxDate)) {        
              
                return <TelemetricDataListItem station={station} 
                  telemetric={this.props.telemetricData[this.props.stations.indexOf(station)]} />
              }}

            )}

          </Col>
        </Row>
      </Container>
    );
  }
}