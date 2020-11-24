import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './Asset.css'
import './AssetList.css'
import './AssetListItem.css'

import AssetListItem from './AssetListItem';

export default class AssetList extends React.Component {

  constructor(props) {
    super(props);
    this.onAssetSelected = this.onAssetSelected.bind(this);
  }

  onAssetSelected(assetState) {
    this.props.assetSelected(assetState);
  }

  render() {

    var query = this.props.query;
    var queryInactive = !this.props.query;

    return(
      <Container className="asset-list-container">
        <Row className="asset-list-row">
          <Col className="asset-list-column">

            {this.props.assets.map(asset => {
              if (queryInactive || asset.deviceId.indexOf(query) > -1) {

                return <AssetListItem 
                  title={asset.deviceId}
                  location={asset.location}
                  description={asset.eventId}
                  assetSelected={this.onAssetSelected}>
                </AssetListItem>
              }
            })
           }
          </Col>
        </Row>
      </Container>
    );
  }
}