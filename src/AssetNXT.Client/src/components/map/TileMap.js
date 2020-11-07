import React, { Component } from "react";
import { Map, TileLayer } from 'react-leaflet';

import './TileMap.css';

export default class TileMap extends Component {

  state = {
    loading: false
  }

  componentDidMount() {
    this.map = this.mapInstance.leafletElement;
  }

  componentDidUpdate() {
  }

  renderComponent(children) {
    return (
      <Map zoom={this.props.zoom}
           center={this.props.center}
           ref={e => this.mapInstance = e}>

        <TileLayer tileSize={512} zoomOffset={-1}
                   url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
                   attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />

        {children}

      </Map>
    );
  }

  render() {

    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderComponent(this.props.children);

    return contents;
  }

  ensureInCenter(location) {
    this.map.panTo(location);
  }

  ensureWithinBounds(locations) {
    const bounds = this.map.getBounds();
    bounds.extend(locations);
    this.map.fitBounds(bounds);
  }
}