import React, { Component } from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';

export class MapNXT extends Component {
	render() {

		const state = { lat: 51.4417378, lng: 5.4750301, zoom: 13 }
		const position = [state.lat, state.lng]

		return (
			<div className="col p-0 map-container">
				<Map center={position} zoom={state.zoom}
					className="h-100">
					<TileLayer
						tileSize={ 512 }
						zoomOffset={ -1 }
						attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors'
						url="https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek"
					/>
					<Marker position={position}>
						<Popup>
							It's a secret to everybody
						</Popup>
					</Marker>
				</Map>
			</div>
        );
	}
}