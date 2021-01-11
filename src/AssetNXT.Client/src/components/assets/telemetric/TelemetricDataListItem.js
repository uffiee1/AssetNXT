import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataListItem.css'

export default class TelemetricDataListItem extends Component {

  render() {

    const {
      telemetric,
      telemetricSource
    } = this.props;

    return (
      
      <Container className="telemetric-item-container">
        <Row className="telemetric-item-row">
          <Col className="telemetric-item-col">
            <i className="fas fa-calendar-alt"></i>
            <label className="telemetric-string">{new Date(telemetricSource.time).toLocaleDateString()}</label>
          </Col>
          <Col className="telemetric-item-col">
            <i className="far fa-clock"></i>
            <label className="telemetric-string">{new Date(telemetricSource.time).toLocaleTimeString()}</label>
          </Col>
          <Col className="telemetric-item-col">
            <i className="fas fa-chart-line"></i>
            <label className="telemetric-string">{telemetric}</label>
          </Col>
        </Row>
      </Container>

    );
  }
}