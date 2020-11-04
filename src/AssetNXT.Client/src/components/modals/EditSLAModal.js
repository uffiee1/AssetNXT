import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Modal, ModalHeader, ModalBody, ModalFooter, Form, Label } from 'reactstrap';
export class EditSLAModal extends Component {

    constructor(props) {
        super(props) 
    }

    render() {
        console.log(this.props.sla);
        return (
            <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
                <Form>
                    <ModalHeader toggle={this.props.toggle}>Edit Configuration</ModalHeader>
                    <ModalBody>
                        <Container>
                            <Row>
                                <Label for="Name" sm={2}>Name</Label>
                                <Col sm={10}>
                                    <Input type="text" name="name" value={this.props.sla.name} required />
                                </Col>
                            </Row>
                            <Container className="rounded border mt-2">
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Temperature</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="temperatureMin" value={this.props.sla.temperatureMin} required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="temperatureMax" value={this.props.sla.temperatureMax} required />
                                    </Col>
                                </Row>
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Humidity</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="humidityMin" value={this.props.sla.humidityMin} required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="humidiyMax" value={this.props.sla.humidityMax} required />
                                    </Col>
                                </Row>
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Pressure</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="pressureMin" value={this.props.sla.pressureMin} required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="pressureMax" value={this.props.sla.pressureMax} required />
                                    </Col>
                                </Row>
                                <Row className="py-2">
                                    <Col className="d-flex justify-content-center"><Button color="primary" block>More options</Button></Col>
                                </Row>
                            </Container>
                        </Container>
                    </ModalBody>
                    <ModalFooter>
                        <Button color="success" onClick={this.toggle}>Create</Button>{' '}
                        <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
                    </ModalFooter>
                </Form>
            </Modal>
        );
    }
}
