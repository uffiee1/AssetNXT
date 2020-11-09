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
    return(
      <Container className="asset-list-container">
        <Row className="asset-list-row">
          <Col className="asset-list-column">
            
          {this.props.assets.map(asset => 
              asset.tags.map(tag => { 
                
                if (!this.props.query || tag.id.indexOf(this.props.query) > -1) {
                  return(
                    <AssetListItem name={tag.id}
                      location={asset.location}
                      description={tag.id}
                      assetSelected={this.onAssetSelected}/>
                  );
                }

              })
            )}

          </Col>
        </Row>
      </Container>
    );
  }
}