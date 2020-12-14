import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataTemplate.css';
import TelemetricDataList from "./TelemetricDataList";
import TelemetricLineGraph from "./graphs/TelemetricLineGraph";
import DatePicker from "../../date/DatePicker";

export default class TelemetricDataTemplate extends Component {

  state = {
    minDate: null,
    maxDate: null
  }

  onMinDateChanged = (moment) => {
    //console.log("Min Date:");
    //console.log(moment);
    //console.log(moment.toDate());
    //console.log(moment.toString());
    //console.log(moment.toISOString());
    this.setState({ minDate: moment.toISOString() });
  }

  onMaxDateChanged = (moment) => {
    //console.log("Max Date:");
    //console.log(moment);
    //console.log(moment.toDate());
    //console.log(moment.toString());
    //console.log(moment.toISOString());
    this.setState({ maxDate: moment.toISOString() });
  }

  render() {
    return (
      <Container className="data-template-container" fluid>
        <Row className="data-template-row">
          <Col lg="8" className="data-template-col">
            <Row className="no-gutters">
              <Col>
                <TelemetricLineGraph stations={this.props.stations}
                  telemetricData={this.props.telemetricData}
                  telemetricName={this.props.telemetricName}
                  telemetricId={this.props.telemetricId}
                  minDate={this.state.minDate}
                  maxDate={this.state.maxDate}
                />
              </Col>
            </Row>
            <Row className="no-gutters">
              <Col>
                <DatePicker
                  minDate={this.state.minDate}
                  maxDate={this.state.maxDate}
                  minDateChanged={this.onMinDateChanged}
                  maxDateChanged={this.onMaxDateChanged} />
              </Col>
            </Row>
          </Col>
          <Col lg="4" className="data-template-col">
            <TelemetricDataList stations={this.props.stations}
              telemetricData={this.props.telemetricData}
              telemetricName={this.props.telemetricName}
              telemetricId={this.props.telemetricId}
              minDate={this.state.minDate}
              maxDate={this.state.maxDate} />
          </Col>
        </Row>
      </Container>
    );
  }
}
