import React, { Component, Fragment } from "react";
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
    currentIndex: 0,
    dropdownOpen: false,
  }

  render() {
    const { tags, tagId,
      telemetricData: telemetrics,
      telemetricParameterChanged,
      telemetricSelectionChanged } = this.props;

    const {
      dropdownOpen,
      dropdownToggle } = this.state;

    const formatter = new Intl.NumberFormat('en-US', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    });

    const temperature = telemetrics[telemetrics.length - 1].temperature;
    const humidity = telemetrics[telemetrics.length - 1].humidity;
    const pressure = telemetrics[telemetrics.length - 1].pressure;

    return (
      <Fragment>
        <div className="telemetric-view-dashboard">
          <Row noGutters>
            <Col>
              <h3>Dashboard</h3>
            </Col>

            {Object.keys(tags).length > 1 &&
              <Fragment>
                <Col xs="auto">
                <div className="d-flex align-items-center">
                  <h4 className="px-2">Tag: </h4>
                  <Dropdown isOpen={dropdownOpen}
                    toggle={() => this.setState({ dropdownOpen: !dropdownOpen })}>
                    <DropdownToggle>
                      {tagId}
                    </DropdownToggle>
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
              </Fragment>
            }

          </Row>
          <Row noGutters>
            <Col>
              <Button block color="primary"
                onClick={e => telemetricParameterChanged("temperature")}>
                <h3>
                  Temperature
                <span className="d-block">
                    {formatter.format(temperature)}&deg;C
                </span>
                </h3>
              </Button>
            </Col>
            <Col>
              <Button block color="primary"
                onClick={e => telemetricParameterChanged("humidity")}>
                <h3>
                  Humidity
                <span className="d-block">
                    {formatter.format(humidity)}%
                </span>
                </h3>
              </Button>
            </Col>
            <Col>
              <Button block color="primary"
                onClick={e => telemetricParameterChanged("pressure")}>
                <h3>
                  Pressure
                <span className="d-block">
                    {pressure} Pa
                </span>
                </h3>
              </Button>
            </Col>
          </Row>
        </div>
      </Fragment >
    );
  }
}