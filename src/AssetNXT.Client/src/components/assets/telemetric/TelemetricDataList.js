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

            {this.props.stations.map(station =>
              <TelemetricDataListItem station={station}
                telemetric={this.props.telemetricData[this.props.stations.indexOf(station)]} />
            )}

          </Col>
        </Row>
      </Container>
    );
  }
}