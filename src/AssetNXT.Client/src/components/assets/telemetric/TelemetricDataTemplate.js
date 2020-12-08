import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './TelemetricDataTemplate.css';
import TelemetricDataList from "./TelemetricDataList";
import TelemetricLineGraph from "./graphs/TelemetricLineGraph";
import DatePicker from "../../date/DatePicker";

export default class TelemetricDataTemplate extends Component {
    render() {
        return (
            <Container className="data-template-container" fluid>
                <Row className="data-template-row">
                    <Col lg="8" className="data-template-col">
                        <TelemetricLineGraph stations={this.props.stations}
                            telemetricData={this.props.telemetricData}
                            telemetricName={this.props.telemetricName}
                            telemetricId={this.props.telemetricId} />
                    </Col>
                    <Col lg="4" className="data-template-col">
                        <TelemetricDataList stations={this.props.stations}
                            telemetricData={this.props.telemetricData}
                            telemetricName={this.props.telemetricName}
                            telemetricId={this.props.telemetricId} />
                    </Col>
                </Row>
                <Row>
                    <Col lg="12" className="data-template-col">
                        <DatePicker />
                    </Col>
                </Row>
            </Container>
        );
    }
}
