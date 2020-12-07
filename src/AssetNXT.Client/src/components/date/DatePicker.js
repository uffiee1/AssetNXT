import React, { Component } from 'react';
import { Container, Col, Row } from 'reactstrap';
import { TextField } from '@material-ui/core';

export default class DatePicker extends Component {
    render() {
        return (
            <Container fluid>
                <Row>
                    <Col xs="auto">
                        <form noValidate>
                            <TextField 
                                id="datetime-local"
                                label="From"
                                type="datetime-local"
                                defaultValue="2015-05-24T10:30"
                                onChange=""
                                InputLabelProps={{
                                    shrink: true,
                                }}
                            />
                        </form>
                    </Col>
                    <Col xs="auto">
                        <form noValidate>
                            <TextField
                                id="datetime-local"
                                label="To"
                                type="datetime-local"
                                defaultValue="2020-05-24T10:30"
                                onChange=""
                                InputLabelProps={{
                                    shrink: true,
                                }}
                            />
                        </form>
                    </Col>
                </Row>
            </Container>
        )
    }
}