import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricList.css'
import './GeometricListItem.css'

import GeometricListItem from './GeometricListItem';

export default class GeometricList extends React.Component {

  render() {
    return(
      <Container className="geometric-list-container">
        <Row className="geometric-list-row">
          <Col className="geometric-list-column">

            {this.props.boundaries && this.props.boundaries.map(boundary => {    
              return <GeometricListItem boundary={boundary}/>
            })
           }
          </Col>
        </Row>
      </Container>
    );
  }
}