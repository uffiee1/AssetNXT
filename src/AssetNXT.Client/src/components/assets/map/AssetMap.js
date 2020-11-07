import React from 'react';
import { Marker, Popup } from 'react-leaflet';

import './AssetMap.css'
import TileMap from "../../map/TileMap";
import AssetTooltip from './AssetTooltip';

export default class AssetMap extends React.Component {

  state = {
    loading: false
  }

  getName(asset) {
    return "<null>";
  }

  getLatLng(asset) {
    return [asset.location.latitude,
            asset.location.longitude];
  }

  componentDidMount() {
    this.ensureWithinBounds();
  }

  componentDidUpdate() {

  }

  renderComponent(assets) {
    return (
      <TileMap zoom={this.props.zoom}
               center={this.getLatLng(assets[0])}
               ref={e => this.mapInstance = e }>

        { assets.map(asset => 
            asset.tags.map(tag => 
              <Marker position={this.getLatLng(asset)}
                      onclick={() => this.ensureInCenter(asset)}>
                <Popup><AssetTooltip asset={asset} tag={tag} /></Popup>
              </Marker>
            )
        )}

      </TileMap>
    );
  }

  render() {

      var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderComponent(this.props.assets);

    return contents;
  }

  ensureInCenter(asset) {
    const location = this.getLatLng(asset);
    this.mapInstance.ensureInCenter(location);
  }

  ensureWithinBounds() {
    const assets = this.props.assets;
    const locations = assets.map((asset) => {
      return this.getLatLng(asset) });

    this.mapInstance.ensureWithinBounds(locations);
  }
}
