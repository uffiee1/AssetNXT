import React, { Component } from "react";
import { FormGroup,  Form, Container, Input, Button, Col } from 'reactstrap';


export class SLAForm extends Component {
    constructor() {
        super();
        this.postData = this.postData.bind(this);
        this.state = {
            minTemperature: '',
            maxTemperature: '',
            minHumidity: '',
            maxHumidity: '',
            minPressure: '',
            maxPressure: ''
        };
    }
    async postData() {
        console.log(JSON.stringify(this.state));
        fetch('https://localhost:44376/api/SLA', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.state)
        })
    }
    render() {
        return (
            <Form>
                <Container>
                    <FormGroup className="row d-flex jusitify-content-center align-items-center">
                        <Col md="3" sm="12" className="p-2"><label className="mb-0">Temperature <span className="text-muted">°C</span>:</label></Col>
                        <Col md="4" sm="12" className="p-2"><Input placeholder="Min" name="minTemperature" required type="number" className="form-control" value={this.state.minTemperature}
                            onChange={e => this.setState({ minTemperature: e.target.value })} /></Col>
                        <Col md="4" sm="12" className="p-2"><Input placeholder="Max" name="maxTemperature" required type="number" className="form-control" value={this.state.maxTemperature}
                            onChange={e => this.setState({ maxTemperature: e.target.value })} /></Col>
                    </FormGroup>
                    <FormGroup className="row d-flex align-items-center">
                        <Col md="3" sm="12" className="p-2"><label className="mb-0">Humidity <span className="text-muted">%</span>:</label></Col>
                        <Col md="4" sm="12" className="p-2"><Input min="1" max="100" name="minHumidity" required placeholder="Min" type="number" className="form-control" value={this.state.minHumidity}
                            onChange={e => this.setState({ minHumidity: e.target.value })} /></Col>
                        <Col md="4" sm="12" className="p-2"><Input min="1" max="100" name="maxHumidity" required placeholder="Max" type="number" className="form-control" value={this.state.maxHumidity}
                            onChange={e => this.setState({ maxHumidity: e.target.value })} /></Col>
                    </FormGroup>
                    <FormGroup className="row d-flex align-items-center">
                        <Col md="3" sm="12" className="p-2"><label className="mb-0">Pressure <span className="text-muted">Pa:</span></label></Col>
                        <Col md="4" sm="12" className="p-2"><Input placeholder="Min" name="minPressure" required type="number" className="form-control" value={this.state.minPressure}
                            onChange={e => this.setState({ minPressure: e.target.value })} /></Col>
                        <Col md="4" sm="12" className="p-2"><Input placeholder="Max" name="maxPressure" required type="number" className="form-control" value={this.state.maxPressure}
                            onChange={e => this.setState({ maxPressure: e.target.value })} /></Col>
                    </FormGroup>
                    <FormGroup className="row">
                        <Col md="12" className="p-2"><Button onClick={this.postData} color="success">Submit</Button></Col>
                    </FormGroup>
                </Container>
                </Form>
            );
         }
}