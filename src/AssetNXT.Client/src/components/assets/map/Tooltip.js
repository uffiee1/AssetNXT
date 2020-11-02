import React, { Component } from "react";
import { Link } from 'react-router-dom';
import { Container, Row, Col } from 'reactstrap';

import "./Tooltip.css";
import { Asset } from "../Asset";
import { LineChart } from "../../graphs/LineChart";

export class Tooltip extends Component {

  render() {
    return(
      <Container className="tooltip-container">

        <Row className="tooltip-row">
           <Asset name={this.props.name}
            description={this.props.description} />
        </Row>

        <Row className="tooltip-row">
          <hr className="w-100" />
        </Row>

        <Row className="tooltip-row flex-nowrap">
          <Col className="tooltip-column">
            <Row><label className="tooltip-property">Temperature: </label></Row>
            <Row><label className="tooltip-property">Humidity: </label></Row>
            <Row><label className="tooltip-property">Pressure: </label></Row>
          </Col>
          <Col className="tooltip-column" xs="auto">
              <Row><label className="tooltip-property-value">{this.props.temperature}&deg;C</label></Row>
              <Row><label className="tooltip-property-value">{this.props.humidity}%</label></Row>
              <Row><label className="tooltip-property-value">{this.props.pressure} Pa</label></Row>
          </Col>
        </Row>

        <Row className="tooltip-row">
          <Col className="tooltip-icon" xs="auto">
            {this.props.outofbounds 
              ? <i className="fa fa-exclamation-triangle fa-2x text-warning"/>
              : <i className="fa fa-check-circle fa-2x text-success"/>
            }
          </Col>
          <Col className="tooltip-link">
            <Link to={`/details/${this.props.deviceId}/`}>More info</Link>
          </Col>
        </Row>

      </Container>
    );
  }
}
