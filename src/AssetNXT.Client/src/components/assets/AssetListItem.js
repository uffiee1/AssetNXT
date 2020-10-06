import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './Asset.css'
import './AssetListItem.css'
import { Asset } from './Asset';

export class AssetListItem extends Component {

  constructor(props) {
    super(props);
    this.onClick = this.onClick.bind(this);
  }

  onClick() {
    console.log("Clicked!");
  }

  render() {
    return (

      <Container className="asset-item-container">
        <Row className="asset-item-row">
          <Col className="asset-item-col"
               onClick={this.onClick}>
            <Asset name={this.props.name}
             icon={this.props.icon}
             description={this.props.description} />
          </Col>
        </Row>
      </Container>

    );
  }
}