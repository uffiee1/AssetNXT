import React, { Component } from "react";
import { Map, TileLayer, Popup, Marker, Circle } from 'react-leaflet';

import "./Mapper.css";
import { Tooltip } from './Tooltip';

export class Mapper extends Component {

  componentDidMount() {
    this.map = this.mapInstance.leafletElement;
  }

  componentWillReceiveProps(props) {
    this.map.closePopup();
    this.map.panTo(props.position);
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
          asset.tags.map(tag => { return(
               <Marker position={[asset.location.latitude, asset.location.longitude]}
                       onClick={e => this.map.panTo(e.target.getLatLng())}>
                <Popup>
                  <Tooltip name={tag.id}
                    description={tag.id}
                    temperature={Math.round(tag.temperature)}
                    humidity={Math.round(tag.humidity)}
                    pressure={Math.round(tag.pressure)}/>
                </Popup>
              </Marker>
            );
          })
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
