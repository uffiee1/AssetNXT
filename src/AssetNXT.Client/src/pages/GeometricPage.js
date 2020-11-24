import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import Layout from '../components/Layout';
import GeometricMap from '../components/geoconfig/GeometricMap';
import GeometricModal from '../components/geoconfig/GeometricModal';

export default class GeometricPage extends Component
{
    static displayName = GeometricPage.displayName

    render() {
      return <Layout dock={<GeometricModal />} />
    }
}
