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

  onAssetSelected(__e) {
    this.props.assetSelected(this.props)
  }

  render() {

    return (
      <Row noGutters className="asset-item-row"
        onClick={this.onAssetSelected}>

        <Col className="asset-item-col">
          <Asset title={this.props.title}
            description={this.props.description} />
        </Col>

        <Col className="asset-item-col" xs="auto">
          {!!this.props.breached
            ? <i className="fa fa-exclamation-triangle fa-lg text-warning" />
            : <i className="fa fa-lg text-success" />
          }
        </Col>

      </Row>

    );
  }
}