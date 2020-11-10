import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Container, Row, Col } from 'reactstrap';

import './AssetMarkerInfo.css';
import Asset from '../Asset';

export default class AssetMarkerInfo extends Component {

  render() {
    return (
       <Container className="tooltip-container">

        <Row className="tooltip-row">
           <Asset title={this.props.tag.id}
            description={this.props.asset.deviceId} />
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
              <Row><label className="tooltip-property-value">{Math.round(this.props.tag.temperature)}&deg;C</label></Row>
              <Row><label className="tooltip-property-value">{Math.round(this.props.tag.humidity)}%</label></Row>
              <Row><label className="tooltip-property-value">{Math.round(this.props.tag.pressure)} Pa</label></Row>
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
            {this.props.link && <Link to={this.props.link}>More info</Link>}
          </Col>
        </Row>

      </Container>
    );
  }
}