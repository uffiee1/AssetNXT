import React, { Component, Fragment } from "react";
import { Row, Col } from "reactstrap";

import './TelemetricView.css';
import './TelemetricDashboard.css';
import AssetMap from "../map/AssetMap";
import AssetMarkerInfo from "../map/AssetMarkerInfo";

export default class TelemetricView extends Component {

  state = {
    tagId: undefined,
    tagParam: 'temperature',
    telemetricParamName: 'Temperature'
  }

  componentDidMount() {
    const { stations } = this.props;
    if (!!stations && stations.length > 0) {
      const latestStation = stations[stations.length - 1];
      const latestStationTagId = latestStation.tags[0].id;
      this.setState({ tagId: latestStationTagId });
    }
  }

  getComponentData(tagId, tagParam) {
    const { stations, routes } = this.props

    const tags = {};
    stations.forEach(station =>
      station.tags.forEach(tag => {
        tags[tag.id] = tags[tag.id] || []
        tags[tag.id].push(tag);
      }));

    const telemetrics = tags[tagId];
    const telemetricLabels = telemetrics.map(tag => {

      const formatter = new Intl.NumberFormat('en-US', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      });

      switch (tagParam.toLowerCase()) {
        case 'temperature':
          return `${formatter.format(tag[tagParam])} \u00B0C`;
        case 'humidity':
          return `${formatter.format(tag[tagParam])}%`;
        case 'pressure':
          return `${tag[tagParam]} Pa`;
        default:
          return `${formatter.format(tag[tagParam])}`;
      }
    });

    return {
      tags, telemetrics, telemetricLabels, 
      boundaries: routes.flatMap(route => route.points)
    };
  }

  render() {

    const { stations,
      componentDataSnapshot: TelemetricDataSnapshot,
      componentDataTemplate: TelemetricDataTemplate } = this.props;

    const { 
      tagId, tagParam, 
      telemetricParamName } = this.state;

    if (!tagId || !tagParam) {
      return <div>No data available...</div>
    }

    const { tags, telemetrics, telemetricLabels, boundaries } 
    = this.getComponentData(tagId, tagParam);

    if (!tags || tags.length === 0) {
      return <div>No data available...</div>
    }

    return (
      <Fragment>
        <div className="telemetric-view">
          <div className="d-flex flex-column">
            <div className="flex-grow-0 p-4">
              <TelemetricDataSnapshot
                tags={tags}
                tagId={tagId}
                telemetricData={telemetrics}
                telemetricDataSource={stations}
                telemetricSelectionChanged={e => this.setState({ tagId: e })}
                telemetricParameterChanged={e => this.setState({ tagParam: e })} />
            </div>

            <div className="flex-grow-1 p-4">
              <TelemetricDataTemplate
                telemetricData={telemetrics}
                telemetricDataPath={tagParam}
                telemetricDataSource={stations}
                telemetricLabels={telemetricLabels}
                telemetricDataName={telemetricParamName}
                telemetricSelectionChanged={e => this.setState({ tagId: e })}
                telemetricParameterChanged={e => this.setState({ tagParam: e })} />
            </div>
          </div>
          <div className="d-flex flex-column">
            <div className="flex-grow-1 p-4">
              <div className="d-flex flex-column box-card h-100">
                <h3>Geometrics:</h3>
                <div className="flex-grow-1">
                  <AssetMap
                    path
                    zoom={20}
                    assets={stations}
                    assetMarkerTemplate={AssetMarkerInfo}
                    boundaries={boundaries}>
                  </AssetMap>
                </div>
              </div>
            </div>
          </div>
        </div>
      </Fragment>
    );
  }
}