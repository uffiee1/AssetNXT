import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './NotificationListItem.css'

export default class NotificationListItem extends Component {

  render() {
    return (
      
      <Container className="notification-item-container">
        <Row className="notification-item-row">
          <Col className="notification-item-col">
            <i className="fas fa-calendar-alt"></i>
            <label className="notification-string">{new Date(this.props.asset.time).toLocaleDateString()}</label>
          </Col>
          <Col className="notification-item-col">
            <i className="far fa-clock"></i>
            <label className="notification-string">{new Date(this.props.asset.time).toLocaleTimeString()}</label>
          </Col>
          <Col className="notification-item-col">
            <i className="fas fa-chart-line"></i>
            <label className="notification-string">{this.props.telemetric}</label>
          </Col>
        </Row>
      </Container>

    );
  }
}