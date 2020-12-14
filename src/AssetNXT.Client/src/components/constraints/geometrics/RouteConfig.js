import React, { Component } from 'react';
import { Container, Row, Col, Button } from 'reactstrap';
import { Modal, ModalHeader, ModalBody } from 'reactstrap';
import { store } from 'react-notifications-component';
import ReactNotification from 'react-notifications-component'

import './RouteConfig.css'
import RouteTable from './RouteTable';
import GeometricMap from './modal/GeometricMap';
import GeometricModal from './modal/GeometricModal';
import GeoApplyModal from './modal/GeoApplyModal';

export default class RouteConfig extends Component {
  state = {
    route: null,
    boundaries: [],
    createModalState: false,
    updateModalState: false,
    applyModalState: false,
  }

  invokeCreateToggle = () => {
    const { createModalState} = this.state;
    this.setState({createModalState: !createModalState});
  }

  invokeUpdateToggle = () => {
    const { updateModalState } = this.state;
    this.setState({ updateModalState: !updateModalState});
    }

  invokeApplyToggle = () => {
        this.setState({ applyModalState: !this.state.applyModalState });
    }

  invokeDeleteToggle = () => {
    const { deleteModalState } = this.state;
    this.setState({ deleteModalState: !deleteModalState });
  }

  invokeStateHasChanged = () => {
    this.props.stateHasChanged();
  }

  render() {
    return(
      <Container className="route-config" fluid>

        <ReactNotification />

        <Modal className="route-modal p-5" 
          isOpen={this.state.createModalState}>    
          <ModalHeader toggle={this.invokeCreateToggle}>
            Create a route
          </ModalHeader>
          <ModalBody>
            <GeometricModal onSubmit={this.addRoute}
              description={''} 
              boundaries={[]}
              name={''}/>
          </ModalBody>
        </Modal>

        { this.state.route && 
          <Modal className="route-modal p-5"
            isOpen={this.state.updateModalState}>
            <ModalHeader toggle={this.invokeUpdateToggle}>
              Update a route
            </ModalHeader>
            <ModalBody>
             <GeometricModal onSubmit={this.updateRoute}
                description={this.state.route.description}
                boundaries={this.state.boundaries}
                name={this.state.route.name}/>
            </ModalBody>
          </Modal>

        }

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
                <Button color="info" disabled={this.state.route ? false : true} onClick={e => this.invokeUpdateToggle()}>
                  <i className="fas fa-edit"></i> Edit
                </Button>
                 </Col>

                <Col xs="auto">
                  <Button color="info" disabled={this.state.route ? false : true} onClick={e => this.invokeApplyToggle()}>
                     <i className="fas fa-check"></i> Apply
                  </Button>
                </Col>

              <Col xs="auto">
                <Button color="danger" disabled={this.state.route ? false : true } onClick={e => this.deleteRoute(this.state.route)}>
                  <i className="fas fa-trash-alt"></i> Delete
                </Button>
              </Col>
            </Row>

            <Row className="route-table">
              <Col className="pt-4">
                  <RouteTable routes={this.props.routes}
                    onRouteSelected={this.selectRoute}/>
              </Col>
                    </Row>
                    <GeoApplyModal geo={this.state.route} isOpen={this.state.applyModalState} toggle={this.invokeApplyToggle} submit={this.applyRoute} />
          </Col>
        </Row>

      </Container>
    );

  }

  addRoute = async (route) => {

    route.devices = ['<null>'];
    route.boundaries = route.boundaries.map(
      boundary => ({...boundary, colour:'dodgerblue'}));

    await this.submitRoute('api/routes', 'POST', route);
    this.invokeCreateToggle();
    this.invokeStateHasChanged();
    }

    applyRoute = async (route) => {
        route.boundaries = this.state.boundaries;
       await this.submitRoute(`api/routes/${this.state.route.id}`, 'PUT', route);
    }

  updateRoute = async (route) => {
    route.devices = this.state.route.devices;
    route.boundaries = route.boundaries.map(
      boundary => ({...boundary, colour: 'dodgerblue'}));

    this.setState({boundaries: []});
    this.setState({boundaries: route.boundaries});

    await this.submitRoute(`api/routes/${this.state.route.id}`, 'PUT', route);
    this.invokeUpdateToggle();
    this.invokeStateHasChanged();
  }

  deleteRoute = async (route) => {

    await fetch(`api/routes/${this.state.route.id}`, {method: 'DELETE'});
    this.setState({route: null, boundaries: []});
    this.invokeStateHasChanged();
  }

  selectRoute = async (route) => {

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

    this.setState({route, boundaries});
  }

  submitRoute = async (request, method, route) => {

    const data = {
      name: route.name,
      devices: route.devices,
      description: route.description,
      points: route.boundaries.map(boundary => {
        return {
          colour: boundary.colour,
          radius: boundary.radius,
          location: {
            latitude: boundary.position.lat,
            longitude: boundary.position.lng
          }
        }
      })
    }
      console.log(data);
    await fetch(request, {
      method: method,
      body: JSON.stringify(data),
      headers: { 'Content-Type': 'application/json' }

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

  }
}