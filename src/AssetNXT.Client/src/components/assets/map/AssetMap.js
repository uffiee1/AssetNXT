import React, { Component } from "react";
import { Map, TileLayer, Marker, Popup, Polyline, Circle } from "react-leaflet";

import "./AssetMap.css";
import MarkerRed from "../../images/marker-icon-red.png"
import MarkerGold from "../../images/marker-icon-gold.png"

var L = require('leaflet');

export default class AssetMap extends Component {

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

  shouldComponentUpdate() {
    return this.renderLock === 0;
  }

  getIcon(asset) {
    const breached = asset.serviceAgreements.some(result =>
      !result.humidity || !result.pressure || !result.temperature
    );

    const icon = !breached
      ? new L.Icon.Default()
      : new L.Icon({
        iconUrl: MarkerRed,
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
      });

    return icon;
  }

  getAssetLatLng(asset) {
    return [
      asset.location.latitude,
      asset.location.longitude];
  }

  renderAssets(assets, AssetMarkerTemplate) {
    const { query } = this.props;
    const queryInective = !query;

    const assetMarkers = assets.filter(asset =>
      queryInective || asset.deviceId.indexOf(query) > -1);

    return assetMarkers.map((asset, idx) =>

      <Marker
        key={`marker-${idx}`}
        icon={this.getIcon(asset)}
        position={this.getAssetLatLng(asset)}
        onClick={() => this.ensureInCenter(asset)}> {

          !!AssetMarkerTemplate &&
          <Popup autoPan={false}>
            <AssetMarkerTemplate
              asset={asset}
              link={`/station/${asset.deviceId}/`}>
            </AssetMarkerTemplate>
          </Popup>
        }
      </Marker>
    );
  }

  renderBoundaries(boundaries) {
    return boundaries.map((boundary, idx) =>
      <Circle
        key={`boundary-${idx}`}
        radius={boundary.radius}
        center={[boundary.location.latitude,
        boundary.location.longitude]} />
    );
  }

  render() {

    const { zoom, center, path } = this.props;
    const { assets, assetMarkerTemplate, boundaries } = this.props;

    return (
      <Map
        zoom={zoom}
        center={center}
        ref={e => this.mapInstance = e}>

        <TileLayer tileSize={512} zoomOffset={-1}
          url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
          attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />

        {boundaries && this.renderBoundaries(boundaries)}
        {assets && this.renderAssets(assets, assetMarkerTemplate)}
        {path && <Polyline positions={assets.map(asset => this.getAssetLatLng(asset))} />}

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