import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataListItem.css'

export default class TelemetricDataListItem extends Component {

  render() {
    return (

      <Container className="telemetric-item-container">
        <Row className="telemetric-item-row">
          <Col className="telemetric-item-col">
            <i className="fas fa-calendar-alt"></i>
            <label className="telemetric-string">{new Date(this.props.station.time).toLocaleDateString()}</label>
          </Col>
          <Col className="telemetric-item-col">
            <i className="far fa-clock"></i>
            <label className="telemetric-string">{new Date(this.props.station.time).toLocaleTimeString()}</label>
          </Col>
          <Col className="telemetric-item-col">
            <i className="fas fa-chart-line"></i>
            <label className="telemetric-string">{Math.round((this.props.telemetric + Number.EPSILON) * 100) / 100}</label>
          </Col>
        </Row>
      </Container>

    );
  }
}