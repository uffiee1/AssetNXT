import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Asset.css';

export class Asset extends Component {

  render() {
    return(
      <Container className="asset-container">
        <Row className="asset-container-row">

          <Col className="asset-icon-container" xs="auto">
            <i className="fa fa-truck fa-2x"/>
          </Col>

          <Col className="asset-contents-container">
            <Row className="asset-contents-row">
              <label className="asset-contents-label">
                {this.props.name}
              </label>
            </Row>

            <Row className="asset-contents-row">
              <p className="asset-contents-description">
                {this.props.description}
              </p>
            </Row>
          </Col>

        </Row>
      </Container>
    );
  }
}
