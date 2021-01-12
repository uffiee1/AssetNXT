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

    const { agreements } = this.props;
    var breached = agreements && agreements.length > 0 && agreements.some(agreement =>
      !agreement.humidity || !agreement.pressure || !agreement.temperature)

    return (

      <Container className="asset-item-container">
        <Row className="asset-item-row"
          onClick={this.onAssetSelected}>

          <Col className="asset-item-col">
            <Asset title={this.props.title}
              description={this.props.description} />
          </Col>

          <Col className="asset-item-col" xs="auto">
            {breached
              ? <i className="fa fa-exclamation-triangle fa-lg text-warning" />
              : <></>
            }
          </Col>

        </Row>
      </Container>

    );
  }
}