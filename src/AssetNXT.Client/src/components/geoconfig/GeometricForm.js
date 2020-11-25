import React, { Component } from "react";
import { Container, Row, Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

import './GeometricForm.css';

export default class GeometricForm extends Component {
  
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
      <Form>
        <Row>
          <Label for="position">Position:</Label>
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
          <Label for="radius">Radius:</Label>
        </Row>
        <Row>
           <Input type="range" min={1} max={5000} step={1}
             onChange={(e) => this.setBoundaryRadius(e.target.value)}
             value={this.props.boundaryRadius}/>
        </Row>
        <Row>
          <FormGroup>
            <Button color={'primary'}>Add</Button>
          </FormGroup>
        </Row>
      </Form>
    );
  }
}