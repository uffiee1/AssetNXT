import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricModal.css'

import GeometricMap from './GeometricMap';
import GeometricForm from './GeometricForm';
import GeometricList from './GeometricList';

export default class GeometricModal extends Component {

  state = {
    positions: []
  }

  geoStateHasChanged = (state) => {
    this.setState({positions: state.markers});
  }

  render() {
    return(
      <Container fluid>
        <Row>
          <Col xs="12" lg="4">
            <div className="aspect-box">
              <GeometricMap zoom={8} center={[52.2129919, 5.2793703]}
                            stateHasChanged={this.geoStateHasChanged}/>
            </div>
          </Col>
          <Col xs="12" lg="8">
            <Row>
              <GeometricForm/>
            </Row>
            <Row>
              <GeometricList points={this.state.positions}/>
            </Row>
          </Col>
        </Row>
      </Container>
    );
  }
}