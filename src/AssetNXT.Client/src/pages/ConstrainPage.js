import React, { Component } from "react";

import Layout from '../components/Layout';
import ConstrainService from '../components/constrains/ConstrainService';

export default class ConstrainPage extends Component{
    static displayName = ConstrainPage.displayName

    render() {
        var constrainService = <ConstrainService />
        return <Layout dock={constrainService} />
    }
}