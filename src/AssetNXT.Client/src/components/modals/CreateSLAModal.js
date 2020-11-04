import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Modal, ModalHeader, ModalBody, ModalFooter, Form, Label } from 'reactstrap';
export class CreateSlaModal extends Component {

    constructor(props) {
        super(props)

    }

    render() {
        return (
            <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
                <Form>
                    <ModalHeader toggle={this.props.toggle}>Create Configuration</ModalHeader>
                    <ModalBody>
                        <Container>
                            <Row>
                                <Label for="Name" sm={2}>Name</Label>
                                <Col sm={10}>
                                    <Input type="text" name="name" placeholder="configuration-01" required />
                                </Col>
                            </Row>
                            <Container className="rounded border mt-2">
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Temperature</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="minTemperature" required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="maxTemperature" required />
                                    </Col>
                                </Row>
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Humidity</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="minTemperature" required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="maxTemperature" required />
                                    </Col>
                                </Row>
                                <Row className="pt-1">
                                    <Label for="Name" sm={12}>Pressure</Label>
                                    <Col sm={6}>
                                        <Label for="minTemperature">Min</Label>
                                        <Input type="number" name="minTemperature" required />
                                    </Col>
                                    <Col sm={6}>
                                        <Label for="maxTemperature">Max</Label>
                                        <Input type="number" name="maxTemperature" required />
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
