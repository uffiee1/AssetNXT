import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Modal, ModalHeader, ModalBody, ModalFooter, Form, Label } from 'reactstrap';
export class DeleteSLAModal extends Component {

    constructor(props) {
        super(props)

    }

    render() {
        return (
            <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
                    <ModalHeader toggle={this.props.toggle}>Delete Configuration</ModalHeader>
                    <ModalBody>
                        <Container>
                           <p> Are you sure you want to delete this SLA template?</p>
                            </Container>
                    </ModalBody>
                    <ModalFooter>
                    <Button color="danger" onClick={this.toggle}>Delete</Button>{' '}
                    <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
                    </ModalFooter>
            </Modal>
        );
    }
}
