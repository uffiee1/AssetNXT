import React, { Component } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { Container, Row, Col } from 'reactstrap';

import './Home.css';
import Layout from '../components/Layout';
import AssetList from "../components/assets/AssetList";
import AssetMap from '../components/assets/map/AssetMap';
import AssetMarkerInfo from "../components/assets/map/AssetMarkerInfo";
import Searchbar from "../components/search/Searchbar";

export default class Home extends Component {
  static displayName = Home.displayName

    state = {
        zoom: this.props.zoom || 14,
        assets: [],
        loading: true,
        settingsModal: false,
        connection: null
    }

    constructor(props) {
        super(props);
        this.searchQuery = this.searchQuery.bind(this);
        this.onAssetAdded = this.onAssetAdded.bind(this);
        this.onAssetRemoved = this.onAssetRemoved.bind(this);
        this.onAssetSelected = this.onAssetSelected.bind(this);
        this.onSignalRConnection = this.onSignalRConnection.bind(this);
    }

    componentDidMount() {
       this.fetchStationData();
       // SignalR connection
       // this.onSignalRConnection();
    }

    async onSignalRConnection() {
        this.connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5001/livestations")
            .withAutomaticReconnect()
            .build();

        if (this.connection) {
            this.connection
                .start()
                .then((result) => {
                    console.log("Connected!");

                    this.connection.on("GetNewRuuviStations", (a) => {

                        // Set the state of the assets with the updated information
                        this.setState(state => {
                            // filters the copied records
                            state.assets = state.assets.filter(function (obj) {
                                return obj.deviceId !== a.deviceId;
                            });
                            return state.assets.unshift(a);
                        });

                    });
                })
                .catch((e) => console.log("Connection failed: ", e));
        }
    }

    searchQuery(query) {
        this.setState({ query: query})
    }

    onAssetAdded(asset) {

    }

    onAssetRemoved(asset) {
    }

    onAssetSelected(asset) {
        this.mapInstance.ensurePopupClosed();
        this.mapInstance.ensureInCenter(asset);
    }

    renderComponent(assets) {
    
        var assetMap = 
          <AssetMap assets={assets} 
            assetMarkerTemplate={AssetMarkerInfo}
                ref={e => this.mapInstance = e}
                zoom={this.state.zoom}
                query={this.state.query}/>

        var assetList = 
          <AssetList assets={assets}
                assetSelected={this.onAssetSelected}
                query={this.state.query} />

        var assetSearch = 
            <Searchbar searchQuery={this.searchQuery} />

        return <Layout dockLeft={assetList} dock={assetMap} search={assetSearch} />
    }

    render() {

         var contents = this.state.loading
          ? <Layout dock={<p><em>Loading...</em></p>}/>
          : this.renderComponent(this.state.assets);

        return contents;
    }

    async fetchStationData() {
        const request = 'api/stations';

        const response = await fetch(request);
        console.log("Fetch Stations Response:");
        console.log(response);

        const data = await response.json();
        console.log("Fetch Stations Data:");
        console.log(data);
        this.setState({ loading: false, assets: data });
    }
     async fetchSlaData(station) {
        let arr = [];
        const request = 'api/configurations/' + station.deviceId;

        await fetch(request).then(response => response.json()).then(data => {
            data.map((i) => {
                if (i.humidity && i.temperature && i.pressure) {
                    arr.push(true);
                } else {
                    arr.push(false);
                }
            })
        });

          
        return arr;
  }
}