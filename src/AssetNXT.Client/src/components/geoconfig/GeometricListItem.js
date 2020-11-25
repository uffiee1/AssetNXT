import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './GeometricList.css'
import './GeometricListItem.css'

export default class GeometricListItem extends React.Component {

  render() {
    return(
     <p>{this.props.boundary.position.lat}, {this.props.boundary.position.lng} - {this.props.boundary.radius}</p>
    );
  }
}