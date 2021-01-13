import React, { Component } from "react";
import {
  Container,
  Row,
  Col,
  Button,
  Input,
  UncontrolledTooltip,
} from "reactstrap";
import { store } from "react-notifications-component";
import ReactNotification from "react-notifications-component";

import "./ConstrainService.css";
import TablePagination from "./TablePagination";
import CreateConstraints from "./actions/createConstraints";
import EditConstraints from "./actions/editConstraints";
import ApplyConstraints from "./actions/applyConstraints";
import DeleteConstraints from "./actions/deleteConstraints";

import "react-notifications-component/dist/theme.css";

export default class ConstraintService extends Component {
  constructor(props) {
    super(props);
    this.tableRef = React.createRef();
    this.state = {
      selected: 0,
      createModal: false,
      editModal: false,
      deleteModal: false,
      applyModal: false,
      isLoaded: false,
      slaTemplates: [],
      tableSlaTemplates: [],
      tablePageIndex: 0,
      error: null,
    };
    this.toggleCreateModal = this.toggleCreateModal.bind(this);
    this.toggleEditModal = this.toggleEditModal.bind(this);
    this.toggleApplyModal = this.toggleApplyModal.bind(this);
    this.toggleDeleteModal = this.toggleDeleteModal.bind(this);
    this.submitSuccess = this.submitSuccess.bind(this);
    this.setTableSlaTemplate = this.setTableSlaTemplate.bind(this);
    this.setIndex = this.setIndex.bind(this);
  }

  componentDidMount() {
    this.fetchData();
  }

  setTableSlaTemplate(value) {
    this.setState({ tableSlaTemplate: value });
  }

  async fetchData() {
    await fetch("api/constraints")
      .then((res) => res.json())
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
            error,
          });
        }
      );
  }

  splitTemplates(array) {
    var i,
      j,
      tempArray = [],
      chunk = 5;
    for (i = 0, j = array.length; i < j; i += chunk) {
      tempArray = [...tempArray, array.slice(i, i + chunk)];
    }
    return tempArray;
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
          onScreen: false,
        },
      });
    } else {
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
          onScreen: false,
        },
      });
    }
  }

  toggleDeleteModal() {
    this.setState({
      deleteModal: !this.state.deleteModal,
    });
  }

  toggleCreateModal() {
    this.setState({
      createModal: !this.state.createModal,
    });
  }

  toggleEditModal() {
    this.setState({
      editModal: !this.state.editModal,
    });
  }

  toggleApplyModal() {
    this.setState({ applyModal: !this.state.applyModal });
  }

  setIndex(value) {
    this.setState({ tablePageIndex: value });
  }

  searchChange(event) {
    let arr = [];
    if (this.state.slaTemplates !== undefined) {
      if (event.target.value === "") {
        this.setState({
          tableSlaTemplates: this.splitTemplates(this.state.slaTemplates),
        });
      } else {
        this.state.slaTemplates.map((sla, i) => {
          if (
            arr.length < 5 &&
            sla.name.toLowerCase().includes(event.target.value.toLowerCase())
          ) {
            arr.push(sla);
          }
        });
        this.setState({ tableSlaTemplates: [arr], tablePageIndex: 0 });
      }
    }
  }

  renderTableContent() {
    if (this.state.isLoaded && this.state.tableSlaTemplates !== undefined) {
      const listItems = this.state.tableSlaTemplates[
        this.state.tablePageIndex
      ].map((slaTemplate, i) => {
        let className = "p-3";
        if (this.state.selected === slaTemplate) className = "p-3 selected";
        return (
          <>
            <Row
              className={className}
              onClick={() => this.setState({ selected: slaTemplate })}
            >
              <Col id={"tooltip" + i.toString()}>{slaTemplate.name}</Col>
              <Col>{slaTemplate.description}</Col>
            </Row>
            <UncontrolledTooltip
              placement="left"
              target={"tooltip" + i.toString()}
            >
              <Container fluid>
                <Row>MinTemp : {slaTemplate.temperatureMin}</Row>
                <Row>MaxTemp : {slaTemplate.temperatureMax}</Row>
                <Row>MinHumid : {slaTemplate.humidityMin}</Row>
                <Row>MaxHumid : {slaTemplate.humidityMax}</Row>
                <Row>MinPress : {slaTemplate.pressureMin}</Row>
                <Row>MaxPress : {slaTemplate.pressureMax}</Row>
              </Container>
            </UncontrolledTooltip>
          </>
        );
      });
      return listItems;
    }
    if (this.state.error)
      return (
        <tr>
          <td>Error</td>
          <td></td>
        </tr>
      );
    return (
      <tr>
        <td>Loading...</td>
      </tr>
    );
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
          </Row>

          <Row className="py-2">
            <Col
              xs="8"
              lg="4"
              className="d-flex justify-content-center align-items-center"
            >
              <Input
                type="search"
                name="search"
                placeholder="search..."
                onChange={(e) => this.searchChange(e)}
                className="nested-input"
              />
              <div className="input-group-btn">
                <Button color="primary" className="nested-button" disabled>
                  <i className="fa fa-search"></i>
                </Button>
              </div>
            </Col>
            <Col md="12" lg="8" className="d-flex justify-content-lg-end  pt-1">
              <Button
                onClick={this.toggleCreateModal}
                color="success"
                className="mr-1"
              >
                Create <i className="fas fa-plus"></i>
              </Button>
              <Button
                onClick={this.toggleEditModal}
                color={this.state.selected ? "info" : "secondary"}
                className="mr-1 text-white"
                disabled={this.state.selected ? false : true}
              >
                Edit <i className="fas fa-edit"></i>
              </Button>
              <Button
                onClick={this.toggleApplyModal}
                color={this.state.selected ? "info" : "secondary"}
                className="mr-1"
                disabled={this.state.selected ? false : true}
              >
                Apply <i class="fas fa-check"></i>
              </Button>
              <Button
                onClick={this.toggleDeleteModal}
                color={this.state.selected ? "danger" : "secondary"}
                className="mr-1"
                disabled={this.state.selected ? false : true}
              >
                Delete <i className="fas fa-trash-alt"></i>
              </Button>
            </Col>
          </Row>

          <Row className="py-1">
            <Col>
              <Container fluid className="table-container">
                <Row>
                  <Col>
                    <Row>
                      <Col className="table-head">
                        <Row className="p-3">
                          <Col>Name</Col>
                          <Col>Description</Col>
                        </Row>
                      </Col>
                    </Row>
                    <Row>
                      <Col className="table-body">
                        {this.renderTableContent()}
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </Container>
            </Col>
          </Row>
          {this.state.isLoaded ? (
            <Row className="py-1">
              <Col>
                <div>
                  <TablePagination
                    min={0}
                    max={this.state.tableSlaTemplates.length}
                    index={this.state.tablePageIndex}
                    setIndex={this.setIndex}
                  />
                  <CreateConstraints
                    isOpen={this.state.createModal}
                    toggle={this.toggleCreateModal}
                    success={this.submitSuccess}
                  />
                  <EditConstraints
                    isOpen={this.state.editModal}
                    toggle={this.toggleEditModal}
                    sla={this.state.selected}
                    success={this.submitSuccess}
                  />
                  <DeleteConstraints
                    setIndex={this.setIndex}
                    isOpen={this.state.deleteModal}
                    toggle={this.toggleDeleteModal}
                    sla={this.state.selected}
                    success={this.submitSuccess}
                  />
                  <ApplyConstraints
                    isOpen={this.state.applyModal}
                    toggle={this.toggleApplyModal}
                    sla={this.state.selected}
                    success={this.submitSuccess}
                  />
                </div>
              </Col>
            </Row>
          ) : (
              <p></p>
            )}
        </Container>
      </Container>
    );
  }
}
