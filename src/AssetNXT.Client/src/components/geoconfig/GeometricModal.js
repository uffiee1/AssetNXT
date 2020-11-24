import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricModal.css'

import GeometricMap from './GeometricMap';
import GeometricForm from './GeometricForm';
import GeometricList from './GeometricList';

export default class GeometricModal extends Component {

  state = {
    positions: [],
    selectedIndex: 0
  }

  geoStateHasChanged = (state) => {
    this.setState({
      positions: state.markers, 
      selectedIndex: state.selectedIndex});
  }

  formStateHasChanged = (state) => {
    this.state.positions[this.state.selectedIndex] = state.position;
    this.setState({positions: this.state.positions});
  }

  render() {
    return(
      <Container fluid>
        <Row>
          <Col xs="12" lg="4">
            <div className="aspect-box">
              <GeometricMap positions={this.state.positions} 
                stateHasChanged={this.geoStateHasChanged}
                zoom={8} center={[52.2129919, 5.2793703]} />
            </div>
          </Col>
          <Col xs="12" lg="8">
            <Row>
              <GeometricForm stateHasChanged={this.formStateHasChanged}
                position={this.state.positions[this.state.selectedIndex] || {lat: 0, lng: 0}}/>
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