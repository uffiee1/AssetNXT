import React, { Component } from "react";
import { Container, Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, Row, Col, Input } from 'reactstrap';

export default class DeleteConstrains extends Component {

    state = {
        isLoaded: false,
        searchQuery: "",
        setStations: [],
        filteredStations: [],
        sla: null,
        stations: [],
    }

    constructor(props) {
        super(props)

    }

    componentDidMount() {
        this.fetchStations();
    }

    componentWillReceiveProps() {
        this.setState({ sla: this.props.sla, tags: this.props.sla.tags });
    }

    renderSearchResults() {
        const listItems = this.state.filteredStations.map((tag,i) => {
            if (i < 5) {
                return (
                    <Row className="mt-1">
                        <Col key={tag.id} xs="9">{tag.id}</Col><Col xs="3" className="d-flex justify-content-end"><Button onClick={() => this.applyData(tag.id)} size="sm" color="info">Apply</Button></Col>
                    </Row>
                )
            }
        })
        return listItems;
    }

    renderApplied() {
        const listItems = this.state.tags.map((device,i) => {
            return (
                <Row className="mt-1">
                    <Col key={i} xs="9">{device}</Col><Col xs="3" className="d-flex justify-content-end"><span className="text-danger font-weight-bold px-3">X</span></Col>
                </Row>
                )
        })
        return listItems;
    }

    changeFilteredTags(input) {
        let filtered = [];
        if(input != "") filtered = this.state.stations.filter((tag) => station.deviceId.includes(input));
        this.setState({ filteredStations: filtered });
    }

    async fetchStations() {
        await fetch("/api/stations/")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        tags: result,
                    });
                },
                () => {
                    this.setState({
                        isLoaded: false
                    });
                }
            )
    }

    async applyData(station) {
        const data = { ...this.state.sla };
        data.stations = [...this.state.stations, station];
        await fetch("api/constrains/" + this.state.sla.id, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)

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
                                    onChange={(e) => this.changeFilteredTags(e.target.value)}
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
                        {this.renderSearchResults()}
                        <Row className="px-2 pt-1">
                            <Col md="12" className="bg-info rounded text-light">Applied tags</Col>
                        </Row>
                        {this.state.sla ? this.renderApplied() : null}
                            </Container>
                    </ModalBody>
                <ModalFooter>
                    <Button color="secondary" onClick={this.props.toggle}>Close</Button>
                    </ModalFooter>
            </Modal>
        );
    }
}
