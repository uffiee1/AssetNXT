import React, { Component } from "react";
import { Container, Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, Row, Col, Input } from 'reactstrap';

export default class DeleteConstrains extends Component {

    state = {
        isLoaded: false,
        searchQuery: "",
        setDevices: [],
        filteredDevices: [],
        geo: null,
        devices: [],
    }

    constructor(props) {
        super(props)

    }

    componentDidMount() {
        this.fetchStations();
    }

    componentWillReceiveProps() {
        if (this.props.geo !== null) {
            this.setState({ geo: this.props.geo, setDevices: this.props.geo.devices })
        }
    }

    renderSearchResults() {
        const listItems = this.state.filteredDevices.map((device, i) => {
            if (i < 5 && !this.state.setDevices.includes(device.id)) {
                  return (
                    <Row className="mt-1">
                        <Col key={device.id} xs="9">{device.id}</Col><Col xs="3" className="d-flex justify-content-end"><Button onClick={() => this.applyData(device)} size="sm" color="info">Apply</Button></Col>
                    </Row>
                )
            }
        })
        return listItems; 
    }

    renderApplied() {
        const listItems = this.state.setDevices.map((deviceId,i) => {
            return (
                <Row className="mt-1">
                    <Col key={deviceId} xs="9">{deviceId}</Col><Col xs="3" className="d-flex justify-content-end"><Button color="link" size="sm" onClick={() => this.deleteData(deviceId)} className="text-danger font-weight-bold px-3">X</Button></Col>
                </Row>
                )
        })
        return listItems;
    }

    changeFilteredDevices(input) {
        let filtered = [];
        if (input != "") filtered = this.state.devices.filter((device) => device.id.toLowerCase().includes(input.toLowerCase()));
        this.setState({ filteredDevices: filtered });
    }

    async fetchStations() {
        await fetch("/api/stations/")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        devices: result,
                    });
                });
               
            
    }

    async applyData(device) {
        const data = { ...this.state.geo };
        data.devices.push(device.id);
        this.setState({ geo: data, setDevices: data.devices });
        await this.props.submit(data);
       
    }
    async deleteData(deviceId) {
        const data = { ...this.state.geo };
        data.devices.splice(data.devices.indexOf(deviceId), 1);;
        this.setState({ geo: data, setDevices: data.devices });
        await this.props.submit(data);

    }

    closeModal() {
        this.setState({ filteredDevices: [], searchQuery: "" });
        this.props.toggle();
    }

    render() {
        return (
            <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
                    <ModalHeader toggle={this.props.toggle}>Apply Configurations</ModalHeader>
                    <ModalBody>
                    <Container>
                        <Row className="my-3">
                            <Col lg="2" className="d-flex">  <p className="my-auto">Apply:</p></Col>
                            <Col md="10" className="d-flex justify-content-center align-items-center">
                                <Input
                                    type="search"
                                    name="search"
                                    placeholder="search..."
                                    onChange={(e) => this.changeFilteredDevices(e.target.value)}
                                    className="nested-input"
                                />
                                <div className="input-group-btn">
                                    <Button color="primary" className="nested-button" disabled>
                                        <i className="fa fa-search"></i>
                                    </Button>
                                </div>
                            </Col>
                        </Row>
                        <Row className="px-2">
                            <Col md="12" className="bg-info rounded text-light">Search Results</Col>
                        </Row>
                        {this.state.isLoaded ? this.renderSearchResults() : null}
                        <Row className="px-2 pt-1">
                            <Col md="12" className="bg-info rounded text-light">Applied devices</Col>
                        </Row>
                        {this.state.isLoaded ? this.renderApplied(): null}
                            </Container>
                    </ModalBody>
                <ModalFooter>
                    <Button color="secondary" onClick={() => this.closeModal()}>Close</Button>
                    </ModalFooter>
            </Modal>
        );
    }
}
