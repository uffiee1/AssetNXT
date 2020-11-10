import React, { Component } from "react";
import { Container, Row, Col, Input, Button } from 'reactstrap';
import { AssetList } from "../AssetList";

import "./Searchbar.css";

export default class Searchbar extends Component {

  constructor(props) {
    super(props)
    this.onSearchQuery = this.onSearchQuery.bind(this);
  }

  onSearchQuery(event) {
    this.props.searchQuery(event.target.value);
  }

    render() {
        return (
            <Container fluid>
              <Row>
                <Col xs="auto" className="searchbar p-0 d-flex align-items-center">
                  
                  <Input
                            type="search"
                            name="search"
                            placeholder="search..."
                            onChange={this.onSearchQuery}
                        />
                        <div className="input-group-btn">
                            <Button color="primary" disabled>
                                <i className="fa fa-search"></i>
                            </Button>
                        </div>
                </Col>
              </Row>
            </Container>
        )
    }
}