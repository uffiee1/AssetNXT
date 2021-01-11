import React, { Component } from "react";
import {
  Container,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Row,
  Col,
  Input,
} from "reactstrap";

export default class DeleteConstraints extends Component {
  state = {
    isLoaded: false,
    searchQuery: "",
    setTags: [],
    filteredTags: [],
    sla: null,
    tags: [],
  };

  componentDidMount() {
    this.fetchStations();
  }

  componentWillReceiveProps() {
    if (this.props.sla.tags !== undefined) {
      this.setState({ sla: this.props.sla, setTags: this.props.sla.tags });
    }
  }

  renderSearchResults() {
    const listItems = this.state.filteredTags.map((tag, i) => {
      console.log(this.state.setTags);
      if (i < 5 && !this.state.setTags.includes(tag)) {
        return (
          <Row className="mt-1">
            <Col key={tag.id} xs="9">
              {tag.id}
            </Col>
            <Col xs="3" className="d-flex justify-content-end">
              <Button
                onClick={() => this.applyData(tag)}
                size="sm"
                color="info"
              >
                Apply
              </Button>
            </Col>
          </Row>
        );
      }
    });
    return listItems;
  }

  renderApplied() {
    const listItems = this.state.setTags.map((tag, i) => {
      return (
        <Row className="mt-1">
          <Col key={tag.id} xs="9">
            {tag.id}
          </Col>
          <Col xs="3" className="d-flex justify-content-end">
            <Button
              color="link"
              size="sm"
              onClick={() => this.deleteData(tag)}
              className="text-danger font-weight-bold px-3"
            >
              X
            </Button>
          </Col>
        </Row>
      );
    });
    return listItems;
  }

  changeFilteredTags(input) {
    let filtered = [];
    if (input !== "")
      filtered = this.state.tags.filter((tag) =>
        tag.id.toLowerCase().includes(input.toLowerCase())
      );
    this.setState({ filteredTags: filtered });
  }

  async fetchStations() {
    await fetch("/api/stations/")
      .then((res) => res.json())
      .then((result) => {
        let arr = [];
        result.map((station) => {
          station.tags.map((tag) => {
            arr.push(tag);
          });
        });
        this.setState({
          isLoaded: true,
          tags: arr,
        });
        console.log(result);
      });
  }

  async applyData(tag) {
    const data = { ...this.state.sla };
    data.tags.push(tag);
    await this.postData(data);
  }
  async deleteData(tag) {
    const data = { ...this.state.sla };
    data.tags.splice(data.tags.indexOf(tag), 1);
    await this.postData(data);
  }
  async postData(sla) {
    await fetch("api/constraints/" + this.state.sla.id, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(sla),
    }).then((response) => {
      if (response.ok) {
        this.props.success(true, "Success");
      } else this.props.success(false, "Something went wrong");
    });
  }

  closeModal() {
    this.setState({ filteredTags: [], searchQuery: "" });
    this.props.toggle();
  }

  render() {
    return (
      <Modal isOpen={this.props.isOpen} toggle={this.props.toggle}>
        <ModalHeader toggle={this.props.toggle}>
          Apply Configurations
        </ModalHeader>
        <ModalBody>
          <Container>
            <Row className="my-3">
              <Col lg="2" className="d-flex">
                {" "}
                <p className="my-auto">Apply:</p>
              </Col>
              <Col
                md="10"
                className="d-flex justify-content-center align-items-center"
              >
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
              <Col md="12" className="bg-info rounded text-light">
                Search Results
              </Col>
            </Row>
            {this.state.isLoaded ? this.renderSearchResults() : null}
            <Row className="px-2 pt-1">
              <Col md="12" className="bg-info rounded text-light">
                Applied tags
              </Col>
            </Row>
            {this.state.isLoaded ? this.renderApplied() : null}
          </Container>
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={() => this.closeModal()}>
            Close
          </Button>
        </ModalFooter>
      </Modal>
    );
  }
}
