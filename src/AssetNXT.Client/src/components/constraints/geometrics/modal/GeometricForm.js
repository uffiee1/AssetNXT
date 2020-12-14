import React, { Component } from "react";
import { Container, Row, Col, FormGroup, Label, Input } from 'reactstrap';

import './GeometricForm.css';

export default class GeometricForm extends Component {
  
  setName(value) {
    if (this.props.onChangeName) {
      this.props.onChangeName(value);
    }
  }

  setDescription(value) {
    if (this.props.onChangeDescription) {
      this.props.onChangeDescription(value);
    }
  }

  setBoundaryRadius(value) {
    if (this.props.onChangeBoundaryRadius) {
      this.props.onChangeBoundaryRadius(value);
    }
  }

  setBoundaryLatitude(value) {
    if (this.props.onChangeBoundaryLatitude) {
      this.props.onChangeBoundaryLatitude(value);
    }
  }

  setBoundaryLongitude(value) {
    if (this.props.onChangeBoundaryLongitude) {
      this.props.onChangeBoundaryLongitude(value);
    }
  }

  render() {
    return(
      <Container fluid>
        <Row>
          <Col xs="auto">
            <FormGroup>
              <Label for="name">Name:</Label>
              <Input type="text" placeholder="Name"
                onChange={(e) => this.setName(e.target.value)}
                value={this.props.name}/>
            </FormGroup>
          </Col>
        </Row>
        <Row>
           <Col xs="auto">
            <FormGroup>
              <Label for="description">Description:</Label>
              <Input type="text" placeholder="Description"
                onChange={(e) => this.setDescription(e.target.value)}
                value={this.props.description}/>
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <hr className="w-100"/>
        </Row>
        <Row>
          <Col xs="auto">
            <Label for="position">Position:</Label>
          </Col>
        </Row>
        <Row>
          <Col xs="auto">
            <FormGroup>
                <Input type="text" placeholder="Latitude"
                 onChange={(e) => this.setBoundaryLatitude(e.target.value)}
                 value={this.props.boundaryLatitude} />
            </FormGroup>
          </Col>
          <Col xs="auto">
              <Input type="text" placeholder="Longitude"
               onChange={(e) => this.setBoundaryLongitude(e.target.value)}
               value={this.props.boundaryLongitude}/>
          </Col>
        </Row>
        <Row>
          <Col xs="auto">
            <Label for="radius">Radius:</Label>
          </Col>
        </Row>
        <Row>
          <Col xs="auto">
             <Input type="range" min={1} max={5000} step={1}
               onChange={(e) => this.setBoundaryRadius(e.target.value)}
               value={this.props.boundaryRadius}/>
          </Col>
        </Row>
      </Container>
    );
  }
}