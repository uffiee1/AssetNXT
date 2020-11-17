import React, { Component } from "react";
import { Container, Button, Modal, ModalHeader, ModalBody, ModalFooter, Form } from 'reactstrap';

export default class DeleteConstrains extends Component {

    constructor(props) {
        super(props)

    }

    async deleteData(e) {
        e.preventDefault()
        await fetch("api/constrains/" + this.props.sla.id, {
            method: 'DELETE',
        }).then(response => {
            if (response.ok) {
                this.props.toggle();
                this.props.success(true,"Template successfully deleted");
            }
            else
                this.props.success(false, "Something went wrong")
        });
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
                    <Form onSubmit={e => this.deleteData(e)}>
                        <Button color="danger" onClick={this.toggle}>Delete</Button>{' '}
                    </Form>
                    <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
                    </ModalFooter>
            </Modal>
        );
    }
}
