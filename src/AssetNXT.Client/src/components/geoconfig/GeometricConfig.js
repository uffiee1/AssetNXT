import React, { Component } from 'react';
import './GeometricConfig.css';
import { Map, Marker, TileLayer } from 'react-leaflet';
import { ButtonToggle, Col, Container, Form, FormGroup, Input, Label, Row, Table } from 'reactstrap';

export default class GeometricConfig extends Component {

    constructor(props) {
        super(props);
        this.state = {
            coordinates: [52.2129919, 5.2793703],
            userCoordinates: []
        };
    }

    onClick(e) {
        console.log(e);
        console.log(e.latlng.lat);
        console.log(e.latlng.lng);
    }

    componentDidMount() {
        navigator.geolocation.getCurrentPosition(function (position) {
            console.log("Latitude is :", position.coords.latitude);
            console.log("Longitude is :", position.coords.longitude);
        });
    }

    render() {
        return (
            <Container className={"h-100"}>
                <Row className={"mt-5 h-50"} >
                    <Col className={"h-100"}>
                        <Map zoom={10}

                            center={this.state.coordinates}>

                            <TileLayer tileSize={512} zoomOffset={-1}
                                url='https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=HfiQgsMsSnorjEs2Sxek'
                                attribution='&amp;copy <a href="https://www.maptiler.com/copyright/">Maptiler</a> contributors' />
                        </Map>
                    </Col>

                    <Col>
                        <Form>
                            <Row form>
                                <Col md={4}>
                                    <FormGroup>
                                        <Label for="streetName">Street</Label>
                                        <Input type="street" name="street" id="streetName" placeholder="Rachelsmolen" />
                                    </FormGroup>
                                </Col>
                            </Row>

                            <Row form>
                                <Col md={4}>
                                    <FormGroup>
                                        <Label for="longitudeValue">Longtitude</Label>
                                        <Input type="longitude" name="longitude" id="longitudeValue" placeholder="x-coordinate" />
                                    </FormGroup>
                                </Col>

                                <Col md={4}>
                                    <FormGroup>
                                        <Label for="latitudeValue">Latitude</Label>
                                        <Input type="latitude" name="latitude" id="latitudeValue" placeholder="y-coordinate" />
                                    </FormGroup>
                                </Col>
                            </Row>

                            <Row form>
                                <Col md={4}>
                                    <FormGroup>
                                        <Label for="radiusValue">Radius</Label>
                                        <Input type="radius" name="radius" id="radiusValue" />
                                        <br />
                                        <Input type="range" min={1} max={100} step={5} />
                                    </FormGroup>
                                </Col>
                            </Row>

                            <div>
                                <ButtonToggle color="info">Create</ButtonToggle>{' '}
                            </div>
                        </Form>
                    </Col>
                </Row>

                <Row className={"row-margin"}>
                    <Table dark>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Longtitude</th>
                                <th>Latitude</th>
                                <th>Radius</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </Table>
                </Row>
            </Container>
        );
    }
}