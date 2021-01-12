import React, { Component } from "react";
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';

import './TelemetricTabs.css';
import 'react-tabs/style/react-tabs.css';

export default class TelemetricTabs extends Component {

  state = {
    selectedIndex: 0
  }

  getTags(stations) {

    var dict = {};
    stations.forEach(station =>
      station.tags.forEach(tag => {
        dict[tag.id] = dict[tag.id] || []
        dict[tag.id].push(tag);
      })
    );

    console.log(dict);
    return dict;
  }

  getPressures(tags, ids) {
    return tags.map(tag => {
      if (ids.includes(tag.id)) {
        return tag.pressure;
      }
    });
  }

  getHumidities(tags, ids) {
    return tags.map(tag => {
      if (ids.includes(tag.id)) {
        return tag.humidity;
      }
    });
  }

  getTemperatures(tags, ids) {
    return tags.map(tag => {
      if (ids.includes(tag.id)) {
        return tag.temperature;
      }
    });
  }

  renderStationTabs(stations, template) {

    var tabs = [];
    var tabPanels = [];
    var tags = this.getTags(stations);

    for (const [key, values] of Object.entries(tags)) {
      tabs.push(<Tab>{key}</Tab>);
      tabPanels.push(template([key], values))
    }

    if (!(Object.keys(tags).length > 1)) {
      return tabPanels.map(content => content);
    }

    else {
      return (
        <Tabs selectedIndex={this.state.selectedIndex}
          onSelect={index => this.setState({ selectedIndex: index })}>

          <TabList>{tabs}</TabList>
          {tabPanels.map(content => <TabPanel>{content}</TabPanel>)}
        </Tabs>
      );
    }

  }

  render() {

    const stationData = this.props.stations;
    const TabPanelTemplate = this.props.telemetricTemplate;

    return (
      <Tabs>
        <TabList>
          <Tab>Humidity</Tab>
          <Tab>Pressure</Tab>
          <Tab>Temperature</Tab>
        </TabList>

        <TabPanel> {
          this.renderStationTabs(stationData, (k, v) =>
            <TabPanelTemplate stations={stationData}
              telemetricData={this.getHumidities(v, k)}
              telemetricName={'humidity'}
              telemetricId={k} />
          )}
        </TabPanel>

        <TabPanel> {
          this.renderStationTabs(stationData, (k, v) =>
            <TabPanelTemplate stations={stationData}
              telemetricData={this.getPressures(v, k)}
              telemetricName={'pressure'}
              telemetricId={k} />
          )}
        </TabPanel>

        <TabPanel> {
          this.renderStationTabs(stationData, (k, v) =>
            <TabPanelTemplate stations={stationData}
              telemetricData={this.getTemperatures(v, k)}
              telemetricName={'temperature'}
              telemetricId={k} />
          )}
        </TabPanel>
      </Tabs>
    );
  }
}