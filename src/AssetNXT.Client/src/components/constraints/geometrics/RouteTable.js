import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './RouteTable.css'

export default class RouteTable extends Component {

  invokeRouteSelected(route) {
    if (this.props.onRouteSelected) {
      this.props.onRouteSelected(route);
    }
  }

  render() {
    return (
      <Container fluid className="table-container">
        <Row>
          <Col>

            <Row>
              <Col className="table-head">
                <Row className="p-3">
                  <Col>Name</Col>
                  <Col>Description</Col>
                </Row>
              </Col>
            </Row>

            <Row>
              <Col className="table-body">

                {this.props.routes &&
                  this.props.routes.map(
                    (route, idx) =>

                      <Row className="p-3"
                        onClick={e => this.invokeRouteSelected(route)}>
                        <Col>{route.name}</Col>
                        <Col>{route.description}</Col>
                      </Row>
                  )}

              </Col>
            </Row>

          </Col>
        </Row>
      </Container>
    );
  }
}