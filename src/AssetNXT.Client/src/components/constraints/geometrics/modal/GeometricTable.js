import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricTable.css'

export default class GeometricTable extends Component {

  invokeBoundaryRemove(index) {
    if (this.props.onRemoveBoundary) {
      this.props.onRemoveBoundary(index);
    }
  }

  invokeBoundaryUpdate(index, boundary) {
    if (this.props.onUpdateBoundary) {
      this.props.onUpdateBoundary(index, boundary);
    }
  }

  render() {
    return(
      <Container fluid className="table-container">
        <Row>
          <Col>

            <Row>
              <Col className="table-head">
                <Row className="p-3">
                  <Col>Latitude</Col>
                  <Col>Longitude</Col>
                  <Col>Radius (m)</Col>
                  <Col/>
                </Row>
              </Col>
            </Row>

            <Row>
              <Col className="table-body">

                {this.props.boundaries &&  
                 this.props.boundaries.map(
                   (boundary, idx) =>

                    <Row className="p-3">
                      <Col>{boundary.position.lat}</Col>
                      <Col>{boundary.position.lng}</Col>
                      <Col>{boundary.radius}</Col>
                      <Col><i className="fas fa-times text-danger"
                         onClick={e => this.invokeBoundaryRemove(idx)}>
                        </i>
                      </Col>
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