import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Layout.css'
import { AssetList } from './assets/AssetList';
import { Mapper } from './assets/map/Mapper';
import { Banner } from "./Banner";
import { SLAForm } from "./SLAForm";

export class Layout extends Component {

  state = {
    tags: [
      { name:"Asset #001", description:"Boschdijktunnel, Eindhoven", position:[51.4417378, 5.4750301], outofbounds:false },
      { name:"Asset #002", description:"Rachelsmolen, Eindhoven", position:[51.451093, 5.4802048], outofbounds:true },
    ],

    zoom: 14,
    position: [51.4417378, 5.4750301]
  }

  constructor() {
    super();
    this.zoomInMark = this.zoomInMark.bind(this);
  }


  async componentDidMount() {

    var tags = this.state.tags;
    await Promise.all(tags.map(async (tag) => {

      // Request new State from API endpoint
      const request = "https://ruuvi-api.herokuapp.com/";
      const response = await fetch(request);
      const data = await response.json();

      // Update properties to new response data
      tag.temperature = Math.round(data.tags.temperature);
      tag.humidity = Math.round(data.tags.humidity);
      tag.pressure = Math.round(data.tags.pressure);

    }));

    // Update state
    this.setState({ tags: tags});
  }

  zoomInMark(assetState) {

    this.setState({position: assetState.position});
  }

  render() {
    return(

      <Container fluid className="layout-container">
        <Row className="layout-container-row">

          <Col xs="12" sm="5" lg="3" xl="2"
               className="layout-sidepanel">
            
            <Row className="layout-sidepanel-banner">
              <Banner src="images/logo.png"/>
            </Row>

            <Row className="layout-sidepanel-contents">
              <AssetList tags={this.state.tags}
                         onAssetSelected={this.zoomInMark}/>
            </Row>

          </Col>

          <Col xs="12" sm="7" lg="9" xl="10"
               className="layout-container-contents
                          d-none d-sm-flex flex-column">

            <Mapper zoom={this.state.zoom} 
                    tags={this.state.tags} 
                    position={this.state.position} />
    
          </Col>

            </Row>
            <SLAForm/>
        </Container>
    );
  }

}