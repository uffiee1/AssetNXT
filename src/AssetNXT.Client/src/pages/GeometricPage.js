import React, { Component } from "react";

import Layout from '../components/Layout';
import GeometricConfig from '../components/geoconfig/GeometricConfig';

export default class GeometricPage extends Component
{
    static displayName = GeometricPage.displayName

    render() {
    var geoConfig = < GeometricConfig />
        return < Layout dock ={geoConfig} />
    }
}
