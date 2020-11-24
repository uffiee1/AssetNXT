import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';
import { Map, TileLayer, Marker, Popup, Polyline, Circle } from 'react-leaflet';

import './GeometricMap.css';

export default class GeometricMap extends Component {

  state = {
    selected: false,
    selectedIndex: Number.NaN,
    markers: this.props.positions
  }

  addMarker(position) {
    console.log('click');
    const index = this.state.markers.length;
    const {markers} = this.state;
    markers.push(position);

    this.setState({markers, selectedIndex: index});
    this.invokeStateHasChanged();
  }
 
  removeMarker(index) {
    const {markers} = this.state;
    markers.splice(index, 1);

    this.setState({markers, selectedIndex: Number.NaN});
    this.invokeStateHasChanged();
  }

  updateMarker(index, position) {
    const {markers} = this.state;
    markers[index] = position;

    this.setState({markers});
    this.invokeStateHasChanged();
  }

  invokeStateHasChanged() {
    if (this.props.stateHasChanged) {
      this.props.stateHasChanged(this.state);
    }
  }

  render() {
    return (
      <Map zoom={this.props.zoom}
        center={this.props.center}
        onclick={(e) => { this.addMarker(e.latlng) }}>

        <TileLayer tileSize={512} zoomOffset={-1}
          url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
          attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />

          {this.state.markers.map((position, idx) => this.renderMarker(position, idx))}
          <Polyline positions={this.state.markers.map(position => [position.lat, position.lng])}/>

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
        ondragstart={this.dragStartHandler}>
      </Marker>
    );
  }

  dragHandler = (__e) => {
    console.log('drag');
    const targetOptions = __e.target.options;
    this.updateMarker(targetOptions.index, __e.latlng);
  }

  dragEndHandler = (__e) => {
    console.log('drag-end');
    const targetOptions = __e.target.options;
    this.setState({selected: false, selectedIndex: targetOptions.index});
    this.invokeStateHasChanged();
  }

  dragStartHandler = (__e) => {
    console.log('drag-start');
    const targetOptions = __e.target.options;
    this.setState({selected: true, selectedIndex: targetOptions.index});
    this.invokeStateHasChanged();

    console.log(this.state.selected);
  }
}