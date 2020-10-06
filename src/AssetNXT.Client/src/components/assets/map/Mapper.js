import React, { Component } from "react";
import { Map, TileLayer, Popup, Marker, Circle } from 'react-leaflet';

import "./Mapper.css";
import { Tooltip } from './Tooltip';

export class Mapper extends Component {
  
  state = {
    zoom: this.props.zoom,
    position: this.props.position ||
      [this.props.lat, this.props.lng]
  }

  constructor(props) {
    super(props)
    this.updatePosition = this.updatePosition.bind(this);
  }

  componentWillReceiveProps(props) {
    this.setState({ position: 
      props.position || [props.lat, props.lng]});
  }

  updatePosition(ev) {
    this.setState({ position: ev.target.center});
  }

  render() {
    return(
    
      <Map zoom={this.state.zoom}
           center={this.state.position}
           ondrag={this.updatePosition}
           className="asset-map-container">

        <TileLayer url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
                   attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors'
                   className="asset-map-tiler" tileSize={512} zoomOffset={-1}/>

        {this.props.tags.map(tag => 
          <Marker position={tag.position}>
            <Popup>
              <Tooltip name={tag.name}
                description={tag.description}
                temperature={tag.temperature}
                humidity={tag.humidity}
                pressure={tag.pressure}/>
            </Popup>
          </Marker>
        )}

        <Circle center={[51.4423204, 5.4777961]}
          color='dodgerblue'
          fillColor='dodgerblue'
          fillOpacity={0.300}
          radius={500}>
        </Circle>


      </Map>
    );
  }
}
