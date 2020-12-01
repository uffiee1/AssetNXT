import React, { Component } from "react";
import { Container, Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, Row, Col, Input } from 'reactstrap';

export default class DeleteConstrains extends Component {

    constructor(props) {
        super(props)
        this.state = {
            isLoaded : false,
            searchQuery: "",
            stations: [],
            filteredStations: [],
            appliedStations: [],
        }

    }

    componentDidMount() {
        this.fetchStations();
    }

    renderSearchResuls() {
        const listItems =  this.state.filteredStations.map((station, i) => {
            console.log(station.deviceId);
            if (i < 5) {
                return (
                    <Row className="mt-1">
                        <Col key={i} xs="9">{station.deviceId}</Col><Col xs="3" className="d-flex justify-content-end"><Button onClick={this.applyData(station.deviceId)} size="sm" color="info">Apply</Button></Col>
                        </Row>
                )
            }
        }
        )
        return listItems;
    }

    changeFilteredDevices(input) {
        let filtered = [];
        if(input != "") filtered = this.state.stations.filter((station) => station.deviceId.includes(input));
        this.setState({ filteredStations: filtered });
    }

    async fetchStations() {
        await fetch("/api/stations/")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        stations: result,
                    });
                },
                () => {
                    this.setState({
                        isLoaded: false
                    });
                }
            )
    }

    async applyData(e) {
        await fetch("api/constrains/", {
            method: 'POST',
        }).then(response => {
            if (response.ok) {
                this.props.toggle();
                this.props.success(true,"Template applied");
            }
            else
                this.props.success(false, "Something went wrong")
        });
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
                        {this.renderSearchResuls()}
                        <Row className="px-2 pt-1">
                            <Col md="12" className="bg-info rounded text-light">Applied devices</Col>
                        </Row>
                            </Container>
                    </ModalBody>
                <ModalFooter>
                    <Button color="secondary" onClick={this.props.toggle}>Close</Button>
                    </ModalFooter>
            </Modal>
        );
    }
}
