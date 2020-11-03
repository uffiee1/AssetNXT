import React, { Component } from "react";
import { SLAForm } from "./SLAForm";
import { FormGroup, Form, Container, Input, Button, Col } from 'reactstrap';


export class SLASideBar extends Component {
    render() {
        return (
            <div>
                <SLAForm/>
            </div>
        );
    }
}