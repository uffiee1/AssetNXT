import React, { Component } from 'react';

import './Home.css'
import Layout from '../components/Layout';
import TileMap from '../components/map/TileMap';
import AssetMap from '../components/assets/map/AssetMap';
import AssetList from '../components/assets/AssetList';

export default class Home extends Component {
  static displayName = Home.name

  state = {
    assets: [],
    loading: true
  }

  constructor(props) {
    super(props);
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
    this.map.ensureInCenter(asset);
  }

  renderComponent(assets) {
    return <Layout dock={ <AssetMap ref={e => this.map = e} zoom={14} assets={assets} />}
                   dockLeft={ <AssetList assets={assets} assetSelected={this.onAssetSelected} /> } />
  }

  render() {

    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
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