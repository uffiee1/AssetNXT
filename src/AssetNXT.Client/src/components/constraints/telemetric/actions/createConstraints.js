import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Modal, ModalHeader, ModalBody, ModalFooter, Form, Label } from 'reactstrap';

import agent from "../../../../api/agent";

export default class CreateConstraints extends Component {

  constructor(props) {
    super(props)
    this.state = {
      name: "",
      description: "",
      tags: [],
      temperatureMin: "",
      temperatureMax: "",
      humidityMin: "",
      humidityMax: "",
      pressureMin: "",
      pressureMax: "",
    }
    this.baseState = this.state;

  }

  async postData(e) {
    e.preventDefault()
    const data = {
      deviceId: "5",
      name: this.state.name,
      tags: this.state.tags,
      description: this.state.description,
      temperatureMin: this.state.temperatureMin,
      temperatureMax: this.state.temperatureMax,
      humidityMin: this.state.humidityMin,
      humidityMax: this.state.humidityMax,
      pressureMin: this.state.pressureMin,
      pressureMax: this.state.pressureMax,
    }
    await fetch(`${agent.baseUrl}/api/constraints`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    }).then(response => {
      if (response.ok) {
        this.setState(this.baseState);
        this.props.toggle();
        this.props.success(true, "Template successfully created");
      }
      else
        this.props.success(false, "Something went wrong")
    });
  }

  render() {
    return (
      <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
        <Form onSubmit={(e) => this.postData(e)}>
          <ModalHeader toggle={this.props.toggle}>Create Configuration</ModalHeader>
          <ModalBody>
            <Container>
              <Row>
                <Label for="Name" sm={2}>Name</Label>
                <Col sm={10}>
                  <Input type="text" name="name" placeholder="configuration-01" onChange={e => this.setState({ name: e.target.value })} value={this.state.name} required />
                </Col>
              </Row>
              <Row className="mt-2">
                <Label for="Name" sm={3}>Description</Label>
                <Col sm={9}>
                  <Input type="textarea" maxLength="75" rows="2" style={{ resize: "none" }} onChange={e => this.setState({ description: e.target.value })} value={this.state.description} name="description" />
                </Col>
              </Row>
              <Container className="rounded border my-2">
                <Row className="pt-1">
                  <Label for="Name" sm={12}>Temperature</Label>
                  <Col sm={6}>
                    <Label for="minTemperature">Min</Label>
                    <Input type="number" name="minTemperature" onChange={e => this.setState({ temperatureMin: e.target.value })} value={this.state.temperatureMin} required />
                  </Col>
                  <Col sm={6}>
                    <Label for="maxTemperature">Max</Label>
                    <Input type="number" name="maxTemperature" onChange={e => this.setState({ temperatureMax: e.target.value })} value={this.state.temperatureMax} required />
                  </Col>
                </Row>
                <Row className="pt-1">
                  <Label for="Name" sm={12}>Humidity</Label>
                  <Col sm={6}>
                    <Label for="minTemperature">Min</Label>
                    <Input type="number" name="minTemperature" onChange={e => this.setState({ humidityMin: e.target.value })} value={this.state.humidityMin} required />
                  </Col>
                  <Col sm={6}>
                    <Label for="maxTemperature">Max</Label>
                    <Input type="number" name="maxTemperature" onChange={e => this.setState({ humidityMax: e.target.value })} value={this.state.humidityMax} required />
                  </Col>
                </Row>
                <Row className="pt-1 pb-4">
                  <Label for="Name" sm={12}>Pressure</Label>
                  <Col sm={6}>
                    <Label for="minTemperature">Min</Label>
                    <Input type="number" name="minTemperature" onChange={e => this.setState({ pressureMin: e.target.value })} value={this.state.pressureMin} required />
                  </Col>
                  <Col sm={6}>
                    <Label for="maxTemperature">Max</Label>
                    <Input type="number" name="maxTemperature" onChange={e => this.setState({ pressureMax: e.target.value })} value={this.state.pressureMax} required />
                  </Col>
                </Row>
                <Row className="d-none">
                  <Col className="d-flex justify-content-center"><Button color="primary" block>More options</Button></Col>
                </Row>
              </Container>
            </Container>
          </ModalBody>
          <ModalFooter>
            <Button color="success">Create</Button>
            <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
          </ModalFooter>
        </Form>
      </Modal>
    );
  }
}
