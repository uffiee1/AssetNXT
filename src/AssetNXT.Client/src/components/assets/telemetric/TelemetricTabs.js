import React, { Component } from "react";
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';

import './TelemetricTabs.css';
import 'react-tabs/style/react-tabs.css';

export default class TelemetricTabs extends Component {

  getPressures(assets) {
     return assets.flatMap(asset => {
      return asset.tags.map(tag => {
        return tag.pressure;
      })
    });
  }

  getHumidities(assets) {
     return assets.flatMap(asset => {
      return asset.tags.map(tag => {
        return tag.humidity;
      })
    });
  }

  getTemperatures(assets) {
     return assets.flatMap(asset => {
      return asset.tags.map(tag => {
        return tag.temperature;
      })
    });
  }

  render() {

    const telemetricData = this.props.assets;
    const TabPanelTemplate = this.props.telemetricTemplate;

    return(
      <Tabs>
        <TabList>
          <Tab>Humidity</Tab>
          <Tab>Pressure</Tab>
          <Tab>Temperature</Tab>
        </TabList>

        <TabPanel>
          <TabPanelTemplate assets={telemetricData} 
            telemetricData={this.getHumidities(telemetricData)}
            telemetricName={'humidity'}/>
        </TabPanel>

        <TabPanel>
          <TabPanelTemplate assets={telemetricData}
            telemetricData={this.getPressures(telemetricData)}
            telemetricName={'pressure'}/>
        </TabPanel>

        <TabPanel>
          <TabPanelTemplate assets={telemetricData}
            telemetricData={this.getTemperatures(telemetricData)}
            telemetricName={'temperature'}/>
        </TabPanel>

      </Tabs>
    );
  }
}
