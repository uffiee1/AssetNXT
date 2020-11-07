import React, { Component } from 'react';

import './AssetPage.css'
import Layout from '../components/Layout';
import { LineChart } from '../components/graphs/LineChart';


export default class AssetPage extends Component {
  static displayName = AssetPage.name

  state = {
    loading: false
  }

  componentDidMount() {
  }

  componentDidUpdate() {
  }

  renderComponent() {
    return <Layout dock={<LineChart/>} />
  }

  render() {
    var contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderComponent();

    return contents;
  }
}