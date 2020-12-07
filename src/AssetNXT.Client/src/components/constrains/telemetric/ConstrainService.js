import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Table } from 'reactstrap';
import { store } from 'react-notifications-component';
import ReactNotification from 'react-notifications-component'

import './ConstrainService.css';
import TablePagination from "./TablePagination";
import CreateConstrains from "./actions/createConstrains";
import EditConstrains from "./actions/editConstrains";
import DeleteConstrains from "./actions/deleteConstrains";

import 'react-notifications-component/dist/theme.css'
export default class ConstrainService extends Component {

    constructor(props) {
        super(props);
        this.tableRef = React.createRef()
        this.state = {
            selected: 0,
            createModal: false,
            editModal: false,
            deleteModal: false,
            isLoaded: false,
            slaTemplates: [],
            tableSlaTemplates: [],
            tablePageIndex: 0,
            error: null
        };
        this.toggleCreateModal = this.toggleCreateModal.bind(this);
        this.toggleEditModal = this.toggleEditModal.bind(this);
        this.toggleDeleteModal = this.toggleDeleteModal.bind(this);
        this.submitSuccess = this.submitSuccess.bind(this);
        this.setTableSlaTemplate = this.setTableSlaTemplate.bind(this);
        this.setIndex = this.setIndex.bind(this);

    }

    componentDidMount() {
        this.table = this.tableRef.current;
        this.fetchData();    
    }

    setTableSlaTemplate(value) {
            this.setState({ tableSlaTemplate: value })
    }

    async fetchData() {
        await fetch("api/constrains")
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        slaTemplates: result,
                        tableSlaTemplates: this.splitTemplates(result),
                        selected: 0,
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: false,
                        error
                    });
                }
            )
    }

    splitTemplates(array) {
        var i, j, tempArray = [], chunk = 10;
        for (i = 0, j = array.length; i < j; i += chunk) {
            tempArray = [...tempArray, array.slice(i, i + chunk)];
        }
        return tempArray
    }

    submitSuccess(success, msg) {
        if (success) {
            this.fetchData();
            store.addNotification({
                title: "Success",
                message: msg,
                type: "success",
                insert: "top",
                container: "bottom-right",
                animationIn: ["animate__animated", "animate__fadeIn"],
                animationOut: ["animate__animated", "animate__fadeOut"],
                dismiss: {
                    duration: 2000,
                    onScreen: false
                }
            });
        }
        else {
            store.addNotification({
                title: "Error",
                message: msg,
                type: "danger",
                insert: "top",
                container: "bottom-right",
                animationIn: ["animate__animated", "animate__fadeIn"],
                animationOut: ["animate__animated", "animate__fadeOut"],
                dismiss: {
                    duration: 2000,
                    onScreen: false
                }
            });
        }
    }

    toggleDeleteModal() {
        this.setState({
            deleteModal: !this.state.deleteModal
        });
    }

    toggleCreateModal() {
        this.setState({
            createModal: !this.state.createModal
        });
    }

    toggleEditModal() {
        this.setState({
            editModal: !this.state.editModal
        });
    }

    setIndex(value) {
        this.setState({ tablePageIndex : value })
    }

    hideOnSearch() {
        if (this.state.tableSlaTemplates[0] === this.state.slaTemplates) return "d-none"
        else return "d-block"
    }

    searchChange(event, table) {
        this.setState({selected : 0})
        var  filter, tr, td, i, txtValue;
        filter = event.target.value.toUpperCase();
        if (filter !== "") this.setState({ tableSlaTemplates: [this.state.slaTemplates], tablePageIndex: 0 })
        else if (filter === "") this.setState({ tableSlaTemplates: this.splitTemplates(this.state.slaTemplates), tablePageIndex: 0 });
        table = this.table;
        tr = table.getElementsByTagName("tr");
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            if (td) {
                txtValue = td.textContent || td.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }

    renderTableContent() {
        if (this.state.isLoaded) {
            const listItems = this.state.tableSlaTemplates[this.state.tablePageIndex].map((slaTemplate) => {
                let className = "";
                if (this.state.selected === slaTemplate) className = "selected";  
                return (
                    <tr className={className} key={slaTemplate.id} onClick={() => this.setState({ selected: slaTemplate })}>
                        <td>{slaTemplate.name}</td>
                        <td>{slaTemplate.description}</td>
                        <td className="tooltiptd">
                            <b>minTemp: {slaTemplate.temperatureMin} </b>
                            <b>maxTemp: {slaTemplate.temperatureMax} </b>
                            <b>minHumid: {slaTemplate.humidityMin} </b>
                            <b>maxHumid: {slaTemplate.humidityMax} </b>
                            <b>minPress: {slaTemplate.pressureMax} </b>
                            <b>maxPress: {slaTemplate.pressureMin} </b>
                        </td>
                    </tr>
                );           
            });
            return listItems;
        }
        if (this.state.error) return <tr><td>Error</td><td></td></tr>
        return <tr><td>Loading...</td></tr>
    }

    render() {
        return (
            <Container fluid>
                <ReactNotification className="mh-90" />
                <Container className="content m-auto">
                    <Row className="py-1">
                        <Col xs="12" lg="6">
                            <h2>Service Level Agreements</h2>
                        </Col>
                        <Col md="12" lg={{ size: 4, offset: 2 }} className="d-flex align-items-end pt-1">
                            <Button onClick={this.toggleCreateModal} color="success" className="mr-1" >Create <i className="fas fa-plus"></i></Button>
                            <Button onClick={this.toggleEditModal} color={this.state.selected ? 'info' : 'secondary'} className="mr-1 text-white" disabled={this.state.selected ? false : true}>Edit <i className="fas fa-edit"></i></Button>
                            <Button onClick={this.toggleDeleteModal} color={this.state.selected ? 'danger' : 'secondary'} className="mr-1" disabled={this.state.selected ? false : true}>Delete <i className="fas fa-trash-alt"></i></Button>
                        </Col>
                    </Row>

                    <Row className="py-2">
                        <Col xs="8" lg="4" className="d-flex justify-content-center align-items-center">
                            <Input
                                type="search"
                                name="search"
                                placeholder="search..."
                                onChange={(e) => this.searchChange(e, this.table)}
                                className="nested-input"
                            />
                            <div className="input-group-btn">
                                <Button color="primary" className="nested-button" disabled>
                                    <i className="fa fa-search"></i>
                                </Button>
                            </div>
                        </Col>
                    </Row>

                    <Row className="py-1">
                        <Col>
                            <Table innerRef={this.tableRef} className="overflow-auto" bordered>
                                <thead>
                                    <tr>
                                        <th className="w-25">Name</th>
                                        <th>Description</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.renderTableContent()}
                                </tbody>
                            </Table>
                        </Col>
                    </Row>
                    {this.state.isLoaded ? (
                    <Row className="py-1">
                        <Col>
                        <div>
                            <div className={this.hideOnSearch()}> <TablePagination min={0} max={this.state.tableSlaTemplates.length} index={this.state.tablePageIndex} setIndex={this.setIndex} /></div>
                        
                            <CreateConstrains isOpen={this.state.createModal} toggle={this.toggleCreateModal} success={this.submitSuccess} />
                            <EditConstrains isOpen={this.state.editModal} toggle={this.toggleEditModal} sla={this.state.selected} success={this.submitSuccess} />
                            <DeleteConstrains setIndex={this.setIndex} isOpen={this.state.deleteModal} toggle={this.toggleDeleteModal} sla={this.state.selected} success={this.submitSuccess} />
                        </div>
                        </Col>
                    </Row>
                        ): <p>Loading...</p>
                    }
                </Container>
            </Container>
        );
    }

}