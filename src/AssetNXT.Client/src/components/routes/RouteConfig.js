import React, { Component } from 'react';
import { Container, Row, Col, Button } from 'reactstrap';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { store } from 'react-notifications-component';
import ReactNotification from 'react-notifications-component'


import './RouteConfig.css'
import RouteTable from './RouteTable';
import GeometricMap from '../geoconfig/GeometricMap';
import GeometricModal from '../geoconfig/GeometricModal';

export default class RouteConfig extends Component {

  state = {
    boundaries: [],
    createModalState: false
  }

  invokeCreateToggle = () => {
    const { createModalState} = this.state;
    this.setState({createModalState: !createModalState});
  }

  invokeUpdate = () => {
    this.props.stateHasChanged();
  }

  render() {
    return(
      <Container className="route-config" fluid>
        <ReactNotification />

        <Modal className="p-5" 
          isOpen={this.state.createModalState}>
          
          <ModalHeader toggle={this.invokeCreateToggle}>
            Create a route
          </ModalHeader>
          <ModalBody>
            <GeometricModal onSubmit={this.addRoute}/>
          </ModalBody>

        </Modal>

        <Row className="py-3">
          <Col xs="4">
            <div className="aspect-box">

              <GeometricMap boundaries={this.state.boundaries}/>

            </div>
          </Col>
          <Col xs="8">

            <Row>

              <Col xs="auto">
                <Button color="primary" onClick={e => this.invokeCreateToggle()}>
                  <i className="fas fa-plus"></i> Add
                </Button>
              </Col>

              <Col xs="auto">
                <Button color="info">
                  <i className="fas fa-edit"></i> Edit
                </Button>
              </Col>

              <Col xs="auto">
                <Button color="danger">
                  <i className="fas fa-trash-alt"></i> Delete
                </Button>
              </Col>
            </Row>

            <Row className="route-table">
              <Col className="pt-4">
                  <RouteTable routes={this.props.routes}
                    onRouteSelected={e => this.selectRoute(e)}/>
              </Col>
            </Row>

          </Col>
        </Row>
      </Container>
    );

  }

  addRoute = async (boundaries) => {
    const data = {
      name: "<null>",
      deviceId: "<null>",
      description: "<null>",
      points: boundaries.map(boundary => {
        return {
          colour: 'dodgerblue',
          radius: boundary.radius,
          location: {
            accuracy: 100,
            latitude: boundary.position.lat,
            longitude: boundary.position.lng
          }
        }
      })
    }

    const request = 'api/routes';
    const requestHeaders = {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    }

    await fetch(request, {
      method: 'POST',
      headers: requestHeaders,
      body: JSON.stringify(data)

    }).then(response => {
      if (response.ok) {
        store.addNotification({
          type: 'success',
          title: 'Success',
          insert: 'top',
          container: 'bottom-right',
          message: 'Route has successfully been created.',
          animationIn: ["animate__animated", "animate__fadeIn"],
          animationOut: ["animate__animated", "animate__fadeOut"],
          dismiss: {
            duration: 2000,
            onScreen: false
          }
        });

        this.setState({
          boundaries: [],
          boundaryRadius: 0,
          boundaryLatitude: 0,
          boundaryLongitude: 0,
          boundaryIndex: Number.NaN
        });

      }
      else {
        store.addNotification({
          type: 'danger',
          title: 'Error',
          insert: 'top',
          container: 'bottom-right',
          message: 'An error has occured while uploading',
          animationIn: ["animate__animated", "animate__fadeIn"],
          animationOut: ["animate__animated", "animate__fadeOut"],
          dismiss: {
            duration: 2000,
            onScreen: false
          }
        });
      }
    });

    this.invokeCreateToggle();
    this.invokeUpdate();
  }

  selectRoute(route) {
    var { boundaries } = this.state;
    boundaries = route.points.map(route => {
      return {
        radius: route.radius,
        position: {
          lat: route.location.latitude,
          lng: route.location.longitude,
        }
      }
    });

    this.setState({boundaries});
  }
}