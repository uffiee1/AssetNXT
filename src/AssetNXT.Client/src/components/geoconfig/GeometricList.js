import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricList.css'
import './GeometricListItem.css'

import GeometricListItem from './GeometricListItem';

export default class GeometricList extends React.Component {

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
      <Container fluid className="geometric-list-container">
        <Row className="geometric-list-row">
          <Col className="geometric-list-column">

            {this.props.boundaries && 
              this.props.boundaries.map((boundary, idx) =>

                <GeometricListItem 
                  boundary={boundary} 
                  boundaryRemove={() => this.invokeBoundaryRemove(idx)}/>
            )
           }
          </Col>
        </Row>
      </Container>
    );
  }
}