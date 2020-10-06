import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Asset.css';


function OobTrue() {
    return <i class="fas fa-exclamation-triangle"></i>
}

function OobFalse() {

    return <i class="fas fa-check-circle"></i>
}

function OutOfBoundsIcon(props) {
    const outOfBounds = props.outOfBounds;
    if (outOfBounds) {
        return <OobTrue/>;
    }
    return <OobFalse />;
}


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

              <label>
                <OutOfBoundsIcon outOfBounds={false}/>
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
