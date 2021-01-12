import React, { Component } from "react";
import {
  Row,
  Col,
  Button,
  Dropdown,
  DropdownMenu,
  DropdownItem,  DropdownToggle,
} from "reactstrap";

export default class TelemetricDashboard extends Component {

  state = {
    dropdownOpen: false
  }

  render() {

    const {
      tags, tagId,
      telemetricData,
      telemetricParameterChanged,
      telemetricSelectionChanged } = this.props;

    const { dropdownOpen } = this.state;
    const formatter = new Intl.NumberFormat('en-US', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    });

    const temperature = telemetricData[telemetricData.length - 1].temperature;
    const humidity = telemetricData[telemetricData.length - 1].humidity;
    const pressure = telemetricData[telemetricData.length - 1].pressure;

    return (
      <div className="telemetric-view-dashboard">
        <Row noGutters>
          <Col>
            <h3>Dashboard</h3>
          </Col>

          {Object.keys(tags).length > 1 &&
            <Col xs="auto">
              <div className="d-flex align-items-center">
                <h4 className="px-2">Tag:</h4>
                <Dropdown
                  isOpen={dropdownOpen}
                  toggle={() => this.setState({ dropdownOpen: !dropdownOpen })}>
                  <DropdownToggle>{tagId}</DropdownToggle>
                  <DropdownMenu>
                    {Object.keys(tags).map((tagId, idx) =>
                      <DropdownItem
                        key={`$drop-down-item-${idx}`}
                        onClick={() => telemetricSelectionChanged(tagId)}>
                        {tagId}
                      </DropdownItem>
                    )}
                  </DropdownMenu>
                </Dropdown>
              </div>
            </Col>
          }

        </Row>
        <Row noGutters>
          <Col xs="12" lg="4">
            <Button color='primary' block onClick={() => telemetricParameterChanged("temperature")}>
              <h3 className="d-flex flex-column">
                <span>Temperature</span>
                <span>{formatter.format(temperature)}&deg;C</span>
              </h3>
            </Button>
          </Col>
          <Col xs="12" lg="4">
            <Button color='primary' block onClick={() => telemetricParameterChanged("humidity")}>
              <h3 className="d-flex flex-column">
                <span>Humidity</span>
                <span>{formatter.format(humidity)}%</span>
              </h3>
            </Button>
          </Col>
          <Col xs="12" lg="4">
            <Button color='primary' block onClick={() => telemetricParameterChanged("pressure")}>
              <h3 className="d-flex flex-column">
                <span>Pressure</span>
                <span>{pressure} Pa</span>
              </h3>
            </Button>
          </Col>
        </Row>
      </div>
    );
  }
}