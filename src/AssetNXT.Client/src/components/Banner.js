import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Banner.css';
import logo from './images/logo.png'

export class Banner extends Component {

  render() {
    return(
      <Container fluid className="banner-container">
        <Row className="banner-container-row">

          <Col className="banner-image-container">
            <img className="img-fluid" src={logo}/>
          </Col>

          <Col className="banner-toggle-container" xs="auto">
            <label htmlFor="toggle" className="m-0">
              <i className="fa-lg fas fa-chevron-left" style={{ cursor: "pointer" }}></i>
              <i className="fa-lg fas fa-chevron-left" style={{ cursor: "pointer" }}></i>
            </label>
          </Col>

        </Row>
      </Container>
    );
  }
}
