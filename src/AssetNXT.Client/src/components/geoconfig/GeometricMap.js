import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';
import { Map, TileLayer, Marker, Popup, Polyline, Circle } from 'react-leaflet';

import './GeometricMap.css';

export default class GeometricMap extends Component {

  defaultZoom = 8

  defaultPosition = [52.2129919, 5.2793703]

  addBoundary(position) {
    const {boundaries} = this.props;
    const index = boundaries.length;

    this.invokeBoundaryAdd({radius: 0, position});
    this.selectBoundary(index);
  }

  selectBoundary(index) {
    if (this.props.onSelectBoundary) {
      this.props.onSelectBoundary(index);
    }
  }

  updateBoundary(index, position) {

    const {boundaries} = this.props;
    const boundary = boundaries[index];

    boundary.position = position;
    this.invokeBoundaryUpdate(index, boundary);
  }

  invokeBoundaryAdd(boundary) {
    if (this.props.onAddBoundary) {
      this.props.onAddBoundary(boundary);
    }
  }

  invokeBoundaryRemove(index) {
    if (this.props.onRemoveBoundary) {
      this.props.onRemoveBoundary(index);
    }
  }

  invokeBoundaryUpdate(index, boundary) {
    if (this.props.onUpdateBoundary) {
      this.props.onUpdateBoundary(index, boundary);
    }
  }

  render() {
    return (
      <Map zoom={this.defaultZoom}
        center={this.defaultPosition}
        onclick={(__e) => this.addBoundary(__e.latlng)}>

        <TileLayer tileSize={512} zoomOffset={-1}
          url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
          attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />

        {this.props.boundaries.map((boundary, idx) => this.renderBoundary(boundary, idx))}
        {this.props.boundaries.map((boundary, idx) => this.renderMarker(boundary.position, idx))}
        <Polyline positions={this.props.boundaries.map(boundary => [boundary.position.lat, boundary.position.lng])} />
      </Map>
    );
  }

  renderMarker(position, idx) {
    return (
      <Marker index={idx}
        draggable={true}
        position={position}
        key={`marker-${idx}`}
        ondrag={this.dragHandler}
        ondragend={this.dragEndHandler}
        ondragstart={this.dragStartHandler}
        onclick={this.clickHandler}>
      </Marker>
    );
  }

  renderBoundary(boundary, idx) {
    return (
      <Circle index={idx} 
        key={`boundary-${idx}`} 
        radius={boundary.radius} 
        center={[boundary.position.lat, 
                 boundary.position.lng]}>
      </Circle>
    );
  }

  clickHandler = (__e) => {
    const options = __e.target.options;
    this.selectBoundary(options.index);
  }

  dragHandler = (__e) => {
    const options = __e.target.options;
    this.updateBoundary(options.index, __e.latlng);
  }

  dragEndHandler = (__e) => {
  }

  dragStartHandler = (__e) => {   
    const options = __e.target.options;
    this.selectBoundary(options.index);
  }
}