import React, { Component } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

import './Home.css';
import Layout from '../components/Layout';
import AssetList from "../components/assets/AssetList";
import AssetMap from '../components/assets/map/AssetMap';
import AssetMarkerInfo from "../components/assets/map/AssetMarkerInfo";
import Searchbar from "../components/search/Searchbar";

import agent from "../api/agent";

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
    this.onSignalRConnection();
  }

  async onSignalRConnection() {
    this.connection = new HubConnectionBuilder()
      //.withUrl("/livestations")
      .withUrl(`${agent.baseUrl}/livestations`)
      .withAutomaticReconnect()
      .build();

    if (this.connection) {
      this.connection
        .start()
        .then((result) => {
          console.log("Connected!");

          this.connection.on("GetNewRuuviStations", async (a) => {
            await this.fetchLocationData(a);
            // Set the state of the assets with the updated information
            this.setState(state => {
              // filters the copied records
              state.assets = state.assets.map(function (obj) {
                return obj.deviceId !== a.deviceId ? obj : a;
              });
              return state.assets;
            });

          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }

  searchQuery(query) {
    this.setState({ query: query })
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
        query={this.state.query} />

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
      ? <Layout dock={<p><em>Loading...</em></p>} />
      : this.renderComponent(this.state.assets);

    return contents;
  }

  async fetchStationData() {
    // const request = 'api/stations';

    // const response = await fetch(request);
    const response = await agent.Stations.getStations();
    console.log("Fetch Stations Response:");
    console.log(response);

    //const data = await response.json;
    const data = await response;
    console.log("Fetch Stations Data:");
    console.log(data);

    data.forEach(async station => {
      await this.fetchLocationData(station);
    });

    this.setState({ loading: false, assets: data });
  }

  async fetchLocationData(station) {
    //const request = `nominatim-endpoint/reverse?lat=${station.location.latitude}&lon=${station.location.longitude}&format=json`;
    //const response = await fetch(request);
    //const data = await response.json();

    //station.location = {
    //  ...station.location, ...data
    //}
  }
}