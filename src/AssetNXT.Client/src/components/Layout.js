import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';
import { Banner } from './Banner';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return(
      this.props.children
    );
  }
}
