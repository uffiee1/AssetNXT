import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Layout.css'
import { AssetList } from './assets/AssetList';
import { Mapper } from './assets/map/Mapper';
import { Banner } from "./Banner";
import { Searchbar } from "./assets/search/Searchbar";

export class Layout extends Component {

  state = {
    state : "",
    zoom : this.props.zoom || 14,
    position: [this.props.assets[0].location.latitude,
               this.props.assets[0].location.longitude]
  }

  constructor(props) {
    super(props);
    this.zoomInMark = this.zoomInMark.bind(this);
    this.searchQuery = this.searchQuery.bind(this);
  }

  zoomInMark(assetState) {
    var location = assetState.location;
    this.setState({ position: [location.latitude, location.longitude] });
  }

  searchQuery(query) {
      this.setState({ query: query})
  }

  render() {
    return(
      <Container fluid className="layout-container">
        <Row className="layout-container-row">

          <Col xs="12" sm="5" lg="3" xl="2"
               className="layout-sidepanel">
            
            <Row className="layout-sidepanel-banner">
              <Banner src="images/logo.png" />
            </Row>
            <Row className="layout-sidepanel-contents">
              <AssetList assets={this.props.assets}
                         assetSelected={this.zoomInMark}
                         query={this.state.query}/>
            </Row>

          </Col>
          <Col xs="12" sm="7" lg="9" xl="10"
               className="layout-container-contents
                          d-none d-sm-flex flex-column">

            <Searchbar searchQuery={this.searchQuery}/>
            <Mapper zoom={this.state.zoom}
                    assets={this.props.assets}
                    query={this.state.query}
                    position={this.state.position}/>

          </Col>
        </Row>
      </Container>
    );
  }
}