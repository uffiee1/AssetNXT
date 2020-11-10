import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './Asset.css'
import './AssetListItem.css'
import Asset from './Asset';

export default class AssetListItem extends Component {

  constructor(props) {
    super(props);
    this.onAssetSelected = this.onAssetSelected.bind(this);
  }

  onAssetSelected() {
    this.props.assetSelected(this.props)
  }

  render() {
    return (
      
      <Container className="asset-item-container">
        <Row className="asset-item-row"
             onClick={this.onAssetSelected}>

          <Col className="asset-item-col">
            <Asset title={this.props.title}
             description={this.props.description} />
          </Col>

        </Row>
      </Container>

    );
  }
}