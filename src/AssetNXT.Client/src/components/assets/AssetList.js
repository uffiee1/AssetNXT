import React, { Component } from "react";
import { Container, Row, Col } from "reactstrap";

import "./Asset.css";
import "./AssetList.css";
import "./AssetListItem.css";

import AssetListItem from "./AssetListItem";
import Asset from "./Asset";

export default class AssetList extends Component {
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

    const { assets } = this.props;

    const filteredAssets = [...assets]
      .filter(asset => queryInactive || asset.deviceId.indexOf(query) > -1)
      .map(asset => {
        var serviceAgreements = asset.serviceAgreements;
        var serviceAgreementsBreached = serviceAgreements && serviceAgreements.length > 0 &&
          serviceAgreements.some(result => !result.pressure || result.humidity || result.temperature);

        var serviceGeometrics = asset.serviceGeometrics;
        var serviceGeometricsBreached = serviceGeometrics && serviceGeometrics.length > 0 &&
          serviceGeometrics.every(result => !result.boundary)

        return ({...asset, breached: serviceGeometricsBreached || serviceAgreementsBreached });
      })
      .sort((a,b) => b.breached - a.breached);

    return (
      <Container className="asset-list-container">
        <Row className="asset-list-row">
          <Col className="asset-list-column">
            {filteredAssets.map((asset, idx) =>
              <AssetListItem
                key={idx}
                title={asset.deviceId}
                breached={asset.breached}
                location={asset.location}
                description={asset.location.display_name}
                assetSelected={this.onAssetSelected}>
              </AssetListItem>
            )}
          </Col>
        </Row>
      </Container>
    );
  }
}
