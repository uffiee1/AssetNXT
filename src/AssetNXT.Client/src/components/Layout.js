import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Layout.css';
import Banner from './Banner';

export default class Layout extends Component {

  render() {
    return(
      <Container fluid className="layout-container">
        <Row className="layout-row-definition">
          <Col className="layout-column-definition">

            <Row className="layout-contents-container">
              { this.dockTop() }
            </Row>

          </Col>
        </Row>
        <Row className="layout-row-definition shrink">
          <Col className="layout-column-definition">
            <Row className="layout-contents-container">

              { this.dockLeft() }
            
              { this.dockCenter() }

              { this.dockRight() }

            </Row>
          </Col>
        </Row>
      </Container>
    );
  }

  dockTop() {

    var contents = (!this.props.dockTop && this.props.dockLeft)
      ? <Col className="layout-contents-panel dock-top" xs="auto"/>
      : <Col className="layout-contents-panel dock-top">
          <Row className="layout-row-definition layout-contents-shadow">

            <Col className="layout-banner" xs="12" sm="5" lg="3" xl="2">
              <Banner src="images/logo.png"/>
            </Col>

            <Col className="layout-component-container">
              {this.props.dockTop}
            </Col>

          </Row>
        </Col>

    return contents;
  }

  dockLeft() {

    var banner = this.props.dockTop
      ? <Col className="layout-banner"/>
      : <Col className="layout-banner">
          <Banner toggle src="images/logo.png"/>
        </Col>

    var contents = !this.props.dockLeft
      ? <Col className="layout-contents-panel dock-left" xs="auto" />
      : <Col className="layout-contents-panel dock-left" xs="12" sm="5" lg="3" xl="2">
          <Row className="layout-row-definition layout-contents-shadow">
            {banner}
          </Row>

          <Row className="layout-row-definition shrink">
            <Col className="layout-component-container">
              {this.props.dockLeft}
            </Col>
          </Row> 

          </Col> 

      return contents;

    var banner = this.props.dockTop
        ? <Col className="layout-banner"/>
    : <Col className="layout-banner">
        <Banner toggle src="images/logo.png"/>
        </Col>
  }

  dockRight() {
    var contents = !this.props.dockRight
      ? <Col className="layout-contents-panel dock-right" xs="auto" />
      : <Col className="layout-contents-panel dock-right" xs="12" sm="5" lg="3" xl="2">

          <Row className="layout-row-definition shrink">
            <Col className="layout-component-container">
              {this.props.dockRight}
            </Col>
          </Row>

        </Col>

    return contents;
  }

  dockCenter() {
     var contents = !this.props.dock
      ? <Col className="layout-contents-panel dock-main" />
      : <Col className="layout-contents-panel dock-main">
           {this.props.dock}
        </Col>

    return contents;
  }
}