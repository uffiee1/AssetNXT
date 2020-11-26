import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Layout.css';
import Navigation from '../components/navigation/Navigation';

export default class Layout extends Component {

  render() {
      return (
      <Container fluid className="layout-container">
        <Navigation />
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

  dockLeft() {

    var contents = !this.props.dockLeft
      ? <Col className="layout-contents-panel dock-left" xs="auto" />
      : <Col className="layout-contents-panel dock-left" xs="12" sm="5" lg="3" xl="2">

          <Row className="layout-row-definition shrink">
            <Col className="layout-component-container">
              {this.props.dockLeft}
            </Col>
          </Row>

        </Col>

    return contents;
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
             {this.props.search}
        </Col>

    return contents;
  }
}