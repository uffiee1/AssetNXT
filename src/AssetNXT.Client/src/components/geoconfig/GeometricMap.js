import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';
import { Map, TileLayer, Marker, Popup, Polyline, Circle } from 'react-leaflet';

import './GeometricMap.css';

export default class GeometricMap extends Component {

  state = {
    markers: []
  }

  addMarker(position) {
    const {markers} = this.state;
    markers.push(position);

    this.setState({markers});
    if (this.props.stateHasChanged) {
      this.props.stateHasChanged(this.state);
    }
  }

  updateMarker(position, index) {
    const {markers} = this.state;
    markers[index] = position;

    this.setState({markers});
    if (this.props.stateHasChanged) {
      this.props.stateHasChanged(this.state);
    }
  }


  renderMarker(position, idx) {
    return (
      <Marker index={idx}
        draggable={true}
        position={position}
        key={`marker-${idx}`}
        ondrag={this.dragHandler}
        ondragend={this.dragEndHandler}
        ondragstart={this.dragStartHandler}>
      </Marker>
    );
  }


  render() {
    return (

      <Map zoom={this.props.zoom}
        center={this.props.center}
        onclick={(e) => this.addMarker(e.latlng)}>

        <TileLayer tileSize={512} zoomOffset={-1}
          url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
          attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />


        {this.state.markers.map((position, idx) => this.renderMarker(position, idx))}
        <Polyline positions={this.state.markers.map(position => [position.lat, position.lng])}/>

      </Map>
    );
  }

  dragHandler = (e) => {
    this.updateMarker(e.latlng, e.target.options.index);
  }

  dragEndHandler = (e) => {
  }

  dragStartHandler = (e) => {
  }

}