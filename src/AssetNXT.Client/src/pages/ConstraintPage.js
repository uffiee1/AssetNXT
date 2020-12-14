import React, { Component } from "react";

import Layout from '../components/Layout';
import ConstraintService from '../components/constraints/telemetric/ConstraintService';

export default class ConstraintPage extends Component{
    static displayName = ConstraintPage.displayName

    render() {
        var constraintService = <ConstraintService />
        return <Layout dock={constraintService} />
    }
}