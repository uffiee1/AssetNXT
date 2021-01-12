import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Container, Row, Col } from 'reactstrap';

import './AssetMarkerInfo.css';
import Asset from '../Asset';

export default class AssetMarkerInfo extends Component {

  state = {
    index: 0,
  }

  moveNext() {

    var maxLength = this.props.asset.tags.length - 1;
    var index = this.state.index + 1;

    index = Math.min(index, maxLength);
    this.setState({ index: index });
  }

  movePrevious() {
    var minLength = 0;
    var index = this.state.index - 1;

    index = Math.max(index, minLength);
    this.setState({ index: index });
  }

  componentDidMount() {
  }



  render() {

    const { asset } = this.props;
    var tag = asset.tags[this.state.index];
    //var agreement = this.props.asset.serviceAgreements[] !== null
    //  ? this.props.asset.serviceAgreements[this.state.index] : null
    var agreements = asset.serviceAgreements
      .filter(agreement => agreement.tagId === tag.id);

    return (
      <Container className="tooltip-container">
        <Row className="tooltip-row flex-nowrap">
          <Col className="tooltip-column p-0">
            <Asset title={this.props.asset.tags[this.state.index].id}
              description={this.props.asset.deviceId} />
          </Col>

          {this.props.asset.tags.length > 1 &&
            <Col className="tooltip-column" xs="auto">
              <span className="tooltip-page">
                {this.state.index + 1}/{this.props.asset.tags.length}
              </span>
            </Col>
          }
        </Row>

        <Row className="tooltip-row">
          <hr className="w-100" />
        </Row>

        <Row className="tooltip-row flex-nowrap">

          {this.props.asset.tags.length > 1 &&
            <Col className="tooltip-column d-flex align-items-center pl-0">
              <button className="tooltip-arrow" onClick={(__e) => this.movePrevious()}>&lt;</button>
            </Col>}

          <Col className="tooltip-column">
            <Row><label className="tooltip-property">Temperature: </label></Row>
            <Row><label className="tooltip-property">Humidity: </label></Row>
            <Row><label className="tooltip-property">Pressure: </label></Row>
          </Col>
          <Col className="tooltip-column" xs="auto">
            <Row><label className={`tooltip-property-value ${!agreements || agreements.every(agreement => agreement.temperature) ? "" : "text-danger"}`}>{Math.round(this.props.asset.tags[this.state.index].temperature)}&deg;C</label></Row>
            <Row><label className={`tooltip-property-value ${!agreements || agreements.every(agreement => agreement.humidity) ? "" : "text-danger"}`}>{Math.round(this.props.asset.tags[this.state.index].humidity)}%</label></Row>
            <Row><label className={`tooltip-property-value ${!agreements || agreements.every(agreement => agreement.pressure) ? "" : "text-danger"}`}>{Math.round(this.props.asset.tags[this.state.index].pressure)} Pa</label></Row>
          </Col>

          {this.props.asset.tags.length > 1 &&
            <Col className="tooltip-column d-flex align-items-center pr-0">
              <button className="tooltip-arrow" onClick={(__e) => this.moveNext()}>&gt;</button>
            </Col>}

        </Row>

        <Row className="tooltip-row flex-nowrap">

          {this.props.asset.tags.length > 1 &&
            <Col xs="auto" className="tooltip-column pl-0 pr-4" />}

          <Col className="tooltip-icon" xs="auto">
            {!!agreements ? (agreements.some(agreement => agreement.humidity) || 
                             agreements.some(agreement => agreement.pressure) || 
                             agreements.some(agreement => agreement.temperature)
              ? <i className="fa fa-exclamation-triangle fa-2x text-warning" />
              : <i className="fa fa-check-circle fa-2x text-success" />
            ) : <i className="fa fa-check-circle fa-2x text-success" />
            }
          </Col>

          <Col className="tooltip-link">
            {this.props.link && <Link to={this.props.link}>More info</Link>}
          </Col>

          {this.props.asset.tags.length > 1 &&
            <Col xs="auto" className="tooltip-column pr-0 pl-4" />}

        </Row>
      </Container>
    );
  }
}