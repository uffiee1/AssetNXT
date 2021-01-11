import React, { Component } from "react";
import { Container, Row, Col } from "reactstrap";

import "./TelemetricDataList.css";
import "./TelemetricDataListItem.css";

import TelemetricDataListItem from "./TelemetricDataListItem";

export default class TelemetricDataList extends Component {
  render() {

    const {
      telemetricLabels,
      telemetricDataSource} = this.props;

    return (
      <Container className="telemetric-list-container">
        <Row className="telemetric-list-row">
          <Col className="telemetric-list-column">
            {telemetricDataSource
              .filter((source) =>
                (!this.props.minDate || source.time > this.props.minDate) &&
                (!this.props.maxDate || source.time < this.props.maxDate))
              .map((source, idx) =>
                <TelemetricDataListItem
                  key={idx}
                  telemetricSource={source}
                  telemetric={telemetricLabels[idx]} />
              )}
          </Col>
        </Row>
      </Container>
    );
  }
}
