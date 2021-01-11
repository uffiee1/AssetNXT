import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './RouteTable.css'
import TablePagination from '../telemetric/TablePagination';

export default class RouteTable extends Component {

   state = {
       tableIndex: 0,
    }

    changeIndex(newVal) {
       this.setState({ tableIndex: newVal });
    }
    invokeRouteSelected(route) {
    if (this.props.onRouteSelected) {
      this.props.onRouteSelected(route);
        }
    }
    constructor(props) {
        super(props);
        this.changeIndex = this.changeIndex.bind(this);
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

    render() {
     const visibleRoutes = this.splitTemplates(this.props.routes);
    return(
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

                {visibleRoutes[this.state.tableIndex] &&
                    visibleRoutes[this.state.tableIndex].map(
                   (route, idx) =>

                     <Row key={idx} className="p-3"
                       onClick={e => this.invokeRouteSelected(route)}>
                      <Col>{route.name}</Col>
                      <Col>{route.description}</Col>
                    </Row>
                )}

                        </Col>
                        <Col sm={12} className="py-2">
                         <TablePagination
                            min={0}
                            max={this.splitTemplates(this.props.routes).length}
                            index={this.state.tableIndex}
                                setIndex={this.changeIndex} />
                            </Col>
              </Row>

                </Col>

            </Row>
      </Container>
    );
  }
}