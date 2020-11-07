import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Tooltip.css';
import { Link } from "react-router-dom";


export default class Tooltip extends Component {

  state = {
    loading: false
  }

  componentDidMount() {
  }

  componentDidUpdate() {
  }

  renderProperties(labels, values) {
    return(
      <Row className="tooltip-row flex-nowrap">
        <Col className="tooltip-column">
          { labels.map(name => <Row className="tooltip-property">{name}</Row> )}
        </Col>
        <Col className="tooltip-column">
          { values.map(value => <Row className="tooltip-property-value"> {value} </Row> )}
        </Col>
      </Row>
    );
  }

  renderComponent(children) {
    return (

      <Container className="tooltip-container">
        <Row className="tooltip-row">
          {children}
        </Row>

        <Row className="tooltip-row">
          <hr className="w-100"/>
        </Row>

        {this.renderProperties(this.props.labels, this.props.values)}

        <Row className="tooltip-row">
          <Col className="tooltip-icon" xs="auto">
            {this.props.error
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

  render() {

    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderComponent(this.props.children);

    return contents;
  }
}