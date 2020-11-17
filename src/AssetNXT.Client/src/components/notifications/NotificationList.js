import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './NotificationList.css'
import './NotificationListItem.css'

import NotificationListItem from './NotificationListItem';

export default class NotificationList extends React.Component {

  render() {
    return(
      <Container className="notification-list-container">
        <Row className="notification-list-row">
          <h4>Notifications</h4>
        </Row>
        <Row className="notification-list-row">
          <Col className="notification-list-column">

            {this.props.assets.map(asset => 
              <NotificationListItem asset={asset} 
                telemetric={this.props.telemetricData[this.props.assets.indexOf(asset)]}/>
            )}

          </Col>
        </Row>
      </Container>
    );
  }
}