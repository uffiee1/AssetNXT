import React, { Component } from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';

import './AssetMap.css'
import AssetMarkerInfo from './AssetMarkerInfo';

export default class AssetMap extends Component {

  state = {
    markers: []
  }

  getAssetLatLng(asset) {
     return [asset.location.latitude,
             asset.location.longitude];
  }

  componentDidMount() {
    this.ensureInCenter(this.props.assets[0]);
    this.ensureWithinBounds(this.props.assets);
  }

  renderAssets(assets, Template) {

    return assets.map(asset => {
      return asset.tags.map(tag =>
        <Marker position={this.getAssetLatLng(asset)}
          onClick={() => this.ensureInCenter(asset)}> {
            
            Template && <Popup>
              <Template asset={asset} tag={tag} 
                        link={`/${asset.deviceId}/`}>
              </Template>
            </Popup>

          }
        </Marker>
      )
    })
  }

  render() {
    return(
      <Map zoom={this.props.zoom}
      center={this.props.position}
      ref={e => this.mapInstance = e}
       className="asset-map-container">

      <TileLayer url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
                  attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors'
                  className="asset-map-tiler" tileSize={512} zoomOffset={-1}/>

        {this.props.assets.map(asset => 
        asset.tags.map(tag => { 

          if (!this.props.query || tag.id.indexOf(this.props.query) > -1) {
            return(
                <Marker position={[asset.location.latitude, asset.location.longitude]}
                        onClick={e => this.map.panTo(e.target.getLatLng())}>
                <Popup>
                  <AssetMarkerInfo name={tag.id}
                    description={tag.id}
                    temperature={Math.round(tag.temperature)}
                    humidity={Math.round(tag.humidity)}
                    pressure={Math.round(tag.pressure)}/>
                </Popup>
              </Marker>
            );
          }
        })
        )}
        
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
      return this.getAssetLatLng(asset) });

    const map = this.mapInstance.leafletElement;
    const bounds = map.getBounds();
    bounds.extend(locations);

    this.mapInstance.leafletElement.fitBounds(bounds);
  }
}