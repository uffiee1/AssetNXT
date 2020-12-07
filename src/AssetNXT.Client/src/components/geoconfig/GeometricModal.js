import React, { Component } from 'react';
import { Container, Row, Col, Button } from 'reactstrap';

import './GeometricModal.css'

import GeometricMap from './GeometricMap';
import GeometricForm from './GeometricForm';
import GeometricTable from './GeometricTable';

export default class GeometricModal extends Component {

  state = {
    name: "",
    deviceId: "",
    boundaries: [],
    description: "",
    boundaryRadius: 0,
    boundaryLatitude: 0,
    boundaryLongitude: 0,
    boundaryIndex: Number.NaN
  }

  componentDidMount() {
    this.setState({
      name: this.props.name,
      deviceId: this.props.deviceId,
      description: this.props.description,
      boundaries: JSON.parse(JSON.stringify(this.props.boundaries))
    });
  }

  render() {
    return (
     <Container fluid className="geometric-modal">
        <Row>
          <Col xs="12" lg="4">
            <div className="aspect-box">
              <GeometricMap draggable
                boundaries={this.state.boundaries}
                boundaryIndex={this.state.boundaryIndex}
                onAddBoundary={this.addBoundary}
                onRemoveBoundary={this.removeBoundary}
                onUpdateBoundary={this.updateBoundary}
                onSelectBoundary={this.selectBoundary} />
            </div>
          </Col>
          <Col xs="12" lg="8">
            <Row className="geometric-form">
              <Col>
                <GeometricForm
                  name={this.state.name}
                  description={this.state.description}
                  boundaryRadius={this.state.boundaryRadius}
                  boundaryLatitude={this.state.boundaryLatitude}
                  boundaryLongitude={this.state.boundaryLongitude}
                  onChangeName={this.changeName}
                  onChangeDescription={this.changeDescription}
                  onChangeBoundaryRadius={this.changeBoundaryRadius}
                  onChangeBoundaryLatitude={this.changeBoundaryLatitude}
                  onChangeBoundaryLongitude={this.changeBoundaryLongitude}>
                </GeometricForm>
              </Col>
            </Row>
            <Row className="geometric-separator">
              <Col>
                <hr className="w-100" />
              </Col>
            </Row>
            <Row className="geometric-table">
              <Col>
                <GeometricTable
                  boundaries={this.state.boundaries}
                  onUpdateBoundary={this.updateBoundary}
                  onRemoveBoundary={this.removeBoundary} />
              </Col>
            </Row>
            <Row className="geometric-add">
              <Col className="py-3">
                <Button color="info" onClick={e => this.submitRoute()}>Submit</Button>
              </Col>
            </Row>
          </Col>
        </Row>
      </Container>
    );
  }

  addBoundary = (boundary) => {
    const { boundaries } = this.state;
    boundaries.push(boundary);
    this.setState({ boundaries });
  }

  removeBoundary = (index) => {
    const { boundaries } = this.state;
    boundaries.splice(index, 1);
    this.setState({ boundaries, boundaryIndex: Number.NaN });
  }

  updateBoundary = (index, boundary) => {
    const { boundaries } = this.state;
    boundaries[index] = boundary;
    this.setState({ boundaries })

    if (this.state.boundaryIndex == index) {
      this.setState({
        boundaryIndex: index,
        boundaryRadius: boundaries[index].radius,
        boundaryLatitude: boundaries[index].position.lat,
        boundaryLongitude: boundaries[index].position.lng
      });
    }
  }

  selectBoundary = (index) => {
    const { boundaries } = this.state;
    this.setState({
      boundaryIndex: index,
      boundaryRadius: boundaries[index].radius,
      boundaryLatitude: boundaries[index].position.lat,
      boundaryLongitude: boundaries[index].position.lng
    });
  }

  submitRoute = () => {
    if (this.props.onSubmit) {
      this.props.onSubmit({
        name: this.state.name,
        boundaries: this.state.boundaries,
        description: this.state.description,
      })
    }
  }

  changeName = (value) => {
    this.setState({name: value});
  }

  changeDescription = (value) => {
    this.setState({ description: value});
  }

  changeBoundaryRadius = (value) => {

    console.log(value);
    this.setState({ boundaryRadius: value });
    if (!isNaN(this.state.boundaryIndex)) {
      const { boundaries, boundaryIndex } = this.state;
      boundaries[boundaryIndex].radius = value;
      this.setState({ boundaries });
    }
  }

  changeBoundaryLatitude = (value) => {
    this.setState({ boundaryLatitude: value });
    if (!isNaN(this.state.boundaryIndex)) {

      const { boundaries, boundaryIndex } = this.state;
      boundaries[boundaryIndex].position.lat = value;
      this.setState({ boundaries });
    }
  }

  changeBoundaryLongitude = (value) => {
    this.setState({ boundaryLongitude: value });
    if (!isNaN(this.state.boundaryIndex)) {

      const { boundaries, boundaryIndex } = this.state;
      boundaries[boundaryIndex].position.lng = value;
      this.setState({ boundaries });
    }
  }
}