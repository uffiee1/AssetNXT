import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';
import { AssetList } from "../AssetList";

import "./Searchbar.css";

export class Searchbar extends Component {

  constructor(props) {
    super(props)
    this.onSearchQuery = this.onSearchQuery.bind(this);
  }

  onSearchQuery(event) {
    this.props.searchQuery(event.target.value);
  }

    render() {
        return (
            <Container fluid className="searchbar">
                <label>test123</label>
                <input type="text" onInput={this.onSearchQuery} />
            </Container>
        )
    }
}