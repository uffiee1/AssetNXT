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
    this.props.onAssetClicked(this.props);
  }

  render() {
    return (

      <Container className="asset-item-container">
        <Row className="asset-item-row">
          <Col className="asset-item-col"
               onClick={this.onClick}>
            <Asset name={this.props.name}
             description={this.props.description} />
          </Col>
        </Row>
      </Container>

    );
  }
}