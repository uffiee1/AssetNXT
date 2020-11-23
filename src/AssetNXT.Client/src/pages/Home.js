import React, { Component } from "react";
import { Container, Row, Col } from 'reactstrap';

import './Home.css'
import Layout from '../components/Layout';
import AssetList from "../components/assets/AssetList";
import AssetMap from '../components/assets/map/AssetMap';
import AssetMarkerInfo from "../components/assets/map/AssetMarkerInfo";
import SettingsButton from "../components/assets/settings/SettingsButton";
import SettingsList from "../components/assets/settings/SettingsList";

export default class Home extends Component {
  static displayName = Home.displayName

  state = {
    assets: [],
      loading: true,
      settingsModal: false,

  }

  constructor(props) {
    super(props);
    this.onAssetAdded = this.onAssetAdded.bind(this);
    this.onAssetRemoved = this.onAssetRemoved.bind(this);
      this.onAssetSelected = this.onAssetSelected.bind(this);
      this.setSettingsModal = this.setSettingsModal.bind(this);
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

      var settingsButton = 
          <SettingsButton setSettingsModal={this.setSettingsModal}/>
         
      var leftPanel = 
        <Container className="d-flex flex-column" fluid>
            <Row className="flex-grow-0">
                {settingsButton}
            </Row>

          <Row className="flex-grow-1">
            {assetList}
              </Row>
         <SettingsList toggle={this.setSettingsModal} isOpen={this.state.settingsModal} />
        </Container>
           
        return <Layout dockLeft={leftPanel} dock={assetMap} />
  }

    setSettingsModal() {
        this.setState({ settingsModal: !this.state.settingsModal });
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