import React, { Component } from "react";
import { Container, Row, Col, Button, Input, Table, Modal, ModalHeader, ModalBody, ModalFooter, Form, FormGroup, Label, FormText} from 'reactstrap';


export class SLAConfig extends Component {
    constructor(props) {
        super(props);
        this.tableRef = React.createRef()
        this.state = {
            selected: null,
            modal: false
        };
        this.toggle = this.toggle.bind(this);

    }
    componentDidMount() {
        this.table = this.tableRef.current;
    }

    toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    searchChange(event, table) {
        var  filter, tr, td, i, txtValue;
        filter = event.target.value.toUpperCase();
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

    isSelected(e) {
        console.log(e.target);
    }
    render() {
        return (
            <Container>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <Form>
                    <ModalHeader toggle={this.toggle}>Create Configuration</ModalHeader>
                    <ModalBody>
                        <Container>
                            <Row>
                                <Label for="Name" sm={2}>Name</Label>
                                <Col sm={10}>
                                    <Input type="text" name="name" placeholder="configuration-01" />
                                </Col>
                            </Row>
                            <Container className="rounded border mt-2">
                            <Row className="pt-1">
                                <Label for="Name" sm={12}>Temperature</Label>
                                <Col sm={6}>
                                    <Label for="minTemperature">Min</Label>
                                    <Input type="number" name="minTemperature" />
                                </Col>
                                <Col sm={6}>
                                    <Label for="maxTemperature">Max</Label>
                                    <Input type="number" name="maxTemperature" />
                                </Col>
                            </Row>
                            <Row className="pt-1">
                                <Label for="Name" sm={12}>Humidity</Label>
                                <Col sm={6}>
                                    <Label for="minTemperature">Min</Label>
                                    <Input type="number" name="minTemperature" />
                                </Col>
                                <Col sm={6}>
                                    <Label for="maxTemperature">Max</Label>
                                    <Input type="number" name="maxTemperature" />
                                </Col>
                            </Row>
                            <Row className="pt-1">
                                <Label for="Name" sm={12}>Pressure</Label>
                                <Col sm={6}>
                                    <Label for="minTemperature">Min</Label>
                                    <Input type="number" name="minTemperature" />
                                </Col>
                                <Col sm={6}>
                                    <Label for="maxTemperature">Max</Label>
                                    <Input type="number" name="maxTemperature" />
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
                        <Button color="secondary" onClick={this.toggle}>Cancel</Button>
                        </ModalFooter>
                   </Form>
                </Modal>
                <Row className="py-1">
                    <Col xs="12" lg="6">
                        <span className="badge badge-primary"><h2>Service Level Agreements</h2></span>                   
                    </Col>
                    <Col md="12" lg={{ size: 4, offset: 2 }} className="d-flex align-items-end pt-1">
                        <Button onClick={this.toggle} color="success" className="mr-1" >Create <i className="fas fa-plus"></i></Button>
                        <Button color={this.state.selected ? 'warning' : 'secondary'} className="mr-1 text-white" disabled={this.state.selected? false : true}>Edit <i className="fas fa-edit"></i></Button>
                        <Button color={this.state.selected ? 'danger' : 'secondary'} className="mr-1" disabled={this.state.selected ? false : true}>Delete <i className="fas fa-trash-alt"></i></Button>
                    </Col>
                </Row>
                <Row className="py-1">
                    <Col xs="8" lg="4" className="d-flex justify-content-center align-items-center">
                        <Input
                            type="search"
                            name="search"
                            placeholder="search..."
                            onChange={(e) => this.searchChange(e, this.table)}
                        />
                        <div className="input-group-btn">
                            <Button color="primary" disabled>
                                <i className="fa fa-search"></i>
                            </Button>
                        </div>
                    </Col>
                </Row>
                <Row className="py-1">
                    <Col>
                        <Table innerRef={this.tableRef} hover>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="Config-01" onClick={() => this.setState({ selected: "Config-01" })}>
                                    <td>Laughing Bacchus Winecellars</td>
                                    <td>Canada</td>
                                </tr>
                                <tr>
                                    <td>Magazzini Alimentari Riuniti</td>
                                    <td>Italy</td>
                                </tr>
                                <tr>
                                    <td>North/South</td>
                                    <td>UK</td>
                                </tr>
                                <tr>
                                    <td>Paris specialites</td>
                                    <td>France</td>
                                </tr>
                            </tbody>
                        </Table>
                        </Col>
                    </Row>
            </Container>
            );
    }
}