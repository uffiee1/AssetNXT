import React, { Component } from 'react';
import { Map, TileLayer, Marker, Popup, } from 'react-leaflet';

import './AssetMap.css'
import Asset from '../Asset';
import MarkerRed from "../../images/marker-icon-red.png"
import MarkerGold from "../../images/marker-icon-gold.png"

var L = require('leaflet');

export default class AssetMap extends Component {

  state = {
    markers: [],
  }

  getAssetLatLng(asset) {
    return [asset.location.latitude,
    asset.location.longitude];
  }

  componentDidMount() {
    this.renderLock = 0;
    this.ensureInCenter(this.props.assets[0]);
    this.ensureWithinBounds(this.props.assets);

    const map = this.mapInstance.leafletElement;

    map.on('zoomend', () => --this.renderLock);
    map.on('zoomstart', () => ++this.renderLock);

    map.on('dragend', () => --this.renderLock);
    map.on('dragstart', () => ++this.renderLock);

  }

  returnIcon(asset) {
    var isBreach = false;
    var Red = new L.Icon({
      iconUrl: MarkerRed,
      iconAnchor: new L.Point(16, 16)
    });
    var Default = new L.Icon.Default();
    asset.serviceAgreements.map(breach => {
      if (!breach.humidity || !breach.pressure || !breach.temperature) {
        isBreach = true
      }
    })
    return isBreach ? Red : Default;
  }

  shouldComponentUpdate() {
    return this.renderLock === 0;
  }

  renderAssets(assets, AssetTemplate) {

    var query = this.props.query;
    var queryInactive = !this.props.query;

    return assets.map((asset, idx) => {
      if (queryInactive || asset.deviceId.indexOf(query) > -1) {

        return <Marker
          key={`marker-${idx}`}
          icon={this.returnIcon(asset)}
          position={this.getAssetLatLng(asset)}
          onclick={() => this.ensureInCenter(asset)}> {

            AssetTemplate &&
            <Popup autoPan={false}>
              <AssetTemplate
                asset={asset}
                link={`/station/${asset.deviceId}/`} />
            </Popup>

          }
        </Marker>
      }
    })
  }

  renderBoundaries() {
  }

  render() {

    var assets = this.props.assets;
    var assetTemplate = this.props.assetMarkerTemplate;

    return (
      <Map zoom={this.props.zoom}
        center={this.props.center}
        ref={e => this.mapInstance = e}>

        <TileLayer tileSize={512} zoomOffset={-1}
          url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
          attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />

        {assetTemplate && this.renderAssets(assets, assetTemplate)}

      </Map>
    );
  }

  ensurePopupClosed() {
    this.mapInstance.leafletElement.closePopup();
  }

  ensureInCenter(asset) {
    const location = this.getAssetLatLng(asset);
    this.mapInstance.leafletElement.panTo(location);
  }

  ensureWithinBounds(assets) {
    const locations = assets.map((asset) => {
      return this.getAssetLatLng(asset)
    });

    const map = this.mapInstance.leafletElement;
    const bounds = map.getBounds();
    bounds.extend(locations);

    this.mapInstance.leafletElement.fitBounds(bounds);
  }
}