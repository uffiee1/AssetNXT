import React, { Component } from "react";

import './Home.css'
import Layout from '../components/Layout';
import AssetList from "../components/assets/AssetList";
import AssetMap from '../components/assets/map/AssetMap';
import AssetMarkerInfo from "../components/assets/map/AssetMarkerInfo";

export default class Home extends Component {
  static displayName = Home.displayName

  state = {
    assets: [],
    loading: true
  }

  constructor(props) {
    super(props);
    this.state = { assets: [], loading: true }
    this.onAssetAdded = this.onAssetAdded.bind(this);
    this.onAssetRemoved = this.onAssetRemoved.bind(this);
    this.onAssetSelected = this.onAssetSelected.bind(this);
  }

  componentDidMount() {
    this.fetchStationData();
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
        ref={e => this.mapInstance = e} zoom={14}/>

    var assetList = 
      <AssetList assets={assets}
        assetSelected={this.onAssetSelected}/>

    return <Layout dockLeft={assetList} dock={assetMap} />
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
    console.log("Response:");
    console.log(response);

    const data = await response.json();
    console.log("Data:");
    console.log(data);

    this.setState({ loading: false, assets: data });
  }
}