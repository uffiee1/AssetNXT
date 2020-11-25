import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricList.css'
import './GeometricListItem.css'

export default class GeometricListItem extends React.Component {

  render() {
    return(
     <Container fluid className="geometric-item-container">
        <Row className="geometric-item-row">

          <Col className="geometric-item-col">
            <label className="m-1">Latitude: </label>
            <span>{this.props.boundary.position.lat}</span>
          </Col>
          <Col className="geometric-item-col">
            <label className="m-1">Longitude: </label>
            <span>{this.props.boundary.position.lng}</span>
          </Col>
          <Col className="geometric-item-col">
            <label className="m-1">Radius: </label>
            <span>{this.props.boundary.radius}</span>
          </Col>
          <Col xs="auto" className="geometric-item-col">
            <i onClick={e => this.props.boundaryRemove()} class="fas fa-times text-danger"></i>
          </Col>
        </Row>
      </Container>
    );
  }
}