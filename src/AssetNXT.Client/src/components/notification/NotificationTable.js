import React, { Component } from "react";
import { Container, Row, Col, Badge, Input, Button } from "reactstrap";
// import TablePagination  from "../components/constraints/telemetric/TablePagination";

export default class NotificationTable extends Component {

  state = {
    notifications: [],
    visibleNotifications: [],
    tableIndex: 0
  }

  componentDidMount() {
    this.fetchNotificationData();
  }

  format(region, time, options) {
    if (time !== undefined) {
      return (new Intl.DateTimeFormat(region, options).format(new Date(time)));
    }
  }

  compare(past) {
    return Math.floor((new Date() - new Date(past)) / (1000 * 60 * 60 * 24))
  }

  mockStatus() {
    return Math.round(Math.random()) > 0 ?
      <h5><Badge className="text-white" color="warning" >Warning</Badge></h5> :
      <h5><Badge color="success">Ok</Badge></h5>
  }

  searchChange(event) {
    let arr = [];
    if (this.state.notifications.length > 0) {
      if (event.target.value === "") {
        this.setState({
          visibleNotifications: this.state.notifications,
        });
      } else {
        this.state.notifications.map((notification, idx) => {
          if (
            arr.length < 5 &&
            notification.title.toLowerCase().includes(event.target.value.toLowerCase())
          ) {
            arr.push(notification);
          }
        });
        this.setState({ visibleNotifications: arr });
      }
    }
  }

  //splitTemplates(array) {
  //  var i,
  //    j,
  //    tempArray = [],
  //    chunk = 5;
  //  for (i = 0, j = array.length; i < j; i += chunk) {
  //    tempArray = [...tempArray, array.slice(i, i + chunk)];
  //  }
  //  return tempArray;
  //}

  render() {
    const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    const hourOptions = { hour: 'numeric', minute: 'numeric' };
    return (
      <Container>
        <Row>
          <Col
            xs="8"
            lg="4"
            className="d-flex align-items-center py-4 pl-0"
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
        </Row>
        <br />
        <Row>
          <Col className="table-head">
            <Row className="p-3">
              <Col>Id</Col>
              <Col>Name</Col>
              <Col>Description</Col>
              <Col>Status</Col>
              <Col>Details</Col>
              <Col></Col>
            </Row>

            <Row>
              <Col className="table-body">
                {this.state.visibleNotifications.map(
                  (notification, idx) =>
                    <Row key={notification.id} className="p-3 text-dark">
                      <Col>{idx}</Col>
                      <Col>{notification.title}</Col>
                      <Col>{notification.description}</Col>
                      <Col>{this.mockStatus()}</Col>
                      <Col className="text-center">
                        {this.format('en-US', notification.createdAt, options)} at {this.format('en-US', notification.createdAt, hourOptions)}
                        <br />
                      Updated {this.compare(notification.updatedAt)} days ago
                    </Col>
                      <Col className="text-center d-flex justify-content-center align-items-center"><span className="text-muted h4">x</span></Col>
                    </Row>
                )}
              </Col>
            </Row>
          </Col>
        </Row>
        
      </Container>);
  }

  async fetchNotificationData() {
    await fetch("api/notifications")
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            notifications: result,
            visibleNotifications: result
          })
        },
        (error) => {
          console.log(error);
        }
      );
  }

  async RemoveNotificationData() {

  }
}