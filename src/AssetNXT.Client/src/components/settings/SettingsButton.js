import React from "react";
import { Container, Row, Col } from "reactstrap";
import "./SettingsButton.css";

export default class SettingsButton extends React.Component {
  render() {
    return (
      <Container className="settings-item-container">
        <Row
          className="settings-item-row"
          onClick={this.props.setSettingsModal}
        >
          <Col className="settings-item-col">
            <i class="fas fa-cogs fa-2x"></i>
          </Col>
        </Row>
      </Container>
    );
  }
}
