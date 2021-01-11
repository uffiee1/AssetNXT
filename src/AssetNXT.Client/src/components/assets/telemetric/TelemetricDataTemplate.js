import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataTemplate.css';
import DatePicker from "../../date/DatePicker";
import TelemetricDataList from "./TelemetricDataList";
import TelemetricLineGraph from "./graphs/TelemetricLineGraph";

export default class TelemetricDataTemplate extends Component {

  state = {
    minDate: null,
    maxDate: null
  }

  onMinDateChanged = (moment) => {
    this.setState({ minDate: moment.toISOString() });
  }

  onMaxDateChanged = (moment) => {
    this.setState({ maxDate: moment.toISOString() });
  }

  render() {
    return (
      <div className="telmetric-data-template">
        <Row noGutters>
          <Col xs="12" lg="8">
            <div className="d-flex h-100 flex-column">
              <h3>Statistics:</h3>
              <div className="flex-grow-1">
                <TelemetricLineGraph {...this.props}
                  minDate={this.state.minDate}
                  maxDate={this.state.maxDate} />
              </div>
              <div className="flex-grow-0">
                <DatePicker
                  minDate={this.state.minDate}
                  maxDate={this.state.maxDate}
                  minDateChanged={this.onMinDateChanged}
                  maxDateChanged={this.onMaxDateChanged} />
              </div>
            </div>
          </Col>
          <Col xs="12" lg="4">
            <div className="d-flex h-100 flex-column">
              <h3> Measurements: </h3>
              <div className="flex-grow-1">
                <TelemetricDataList {...this.props}
                  minDate={this.state.minDate}
                  maxDate={this.state.maxDate} />
              </div>
            </div>
          </Col>
        </Row>
      </div>
    );
  }
}
