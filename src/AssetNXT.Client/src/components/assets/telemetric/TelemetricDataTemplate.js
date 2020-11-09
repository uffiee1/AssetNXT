import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataTemplate.css';
import TelemetricLineGraph from "./graphs/TelemetricLineGraph";
import NotificationList from "../../notifications/NotificationList";


export default class TelemetricDataTemplate extends Component {

  render() {
    return (
      <Container className="data-template-container" fluid>
        <Row className="data-template-row">
          <Col lg="8" className="data-template-col">
            <TelemetricLineGraph assets={this.props.assets} 
              telemetricData={this.props.telemetricData}
              telemetricName={this.props.telemetricName} />
          </Col>
          <Col lg="4" className="data-template-col">
            <NotificationList assets={this.props.assets} 
              telemetricData={this.props.telemetricData}
              telemetricName={this.props.telemetricName} />
          </Col>
        </Row>
      </Container>
    );
  }
}
