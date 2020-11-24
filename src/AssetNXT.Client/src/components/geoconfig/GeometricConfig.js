import React, { useState } from "react";
import "./GeometricConfig.css";
import { produce } from "immer";
import { Map, Marker, TileLayer} from "react-leaflet";
import { ButtonToggle, Col, Container, Form, FormGroup, Input, Label, Row, Table } from 'reactstrap';
import { point } from "leaflet";

const GeometricConfig = () => {
    const [coordinates, setCoordinates] = useState([52.2129919, 5.2793703]);

    const [longtitudeState, setLongtitudeState] = useState(0);
    const [latitudeState, setLatitudeState] = useState(0);
    const [radiusState, setRadiusState] = useState(10);

    const pointLocation = { accuracy: "", latitude: "", longtitude: "" };
    function locationReducer(location, latitude, longtitude) {
        return produce(location, draftLocation => {
            draftLocation.latitude = latitude;
            draftLocation.longtitude = longtitude;
        });
    }

    const pointBoundary = { radius: "", colour: "", location: null };
    const [boundaryState, setBoundaryState] = useState(pointBoundary);
    function boundaryReducer(boundary, radius, location) {
        return produce(boundary, draftBoundary => {
            draftBoundary.radius = radius;
            draftBoundary.location = location;
        });
    }

    const route = { deviceId: "", point: [] }
    const [routeState, setRouteState] = useState(route);

    function routeReducer(route, boundary) {
        return produce(route, draftRoute => {
            draftRoute.point.push(boundary);
        });
    }

    function addPoint() {
        const updatedLocation = locationReducer(pointLocation, latitudeState, longtitudeState);
        const updatedBoundary = boundaryReducer(pointBoundary, radiusState, updatedLocation);
        const updatedRoute = routeReducer(routeState, updatedBoundary);
        setRouteState(updatedRoute);
        console.log(updatedRoute);
        console.log(routeState);
    }

    return (
        <Container className={"h-100"}>
            <Row className={"mt-5 h-50"} >
                <Col className={"h-100"}>
                    <Map zoom={10} center={coordinates}>
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
                                    <Label for="longitudeValue">Longtitude</Label>
                                    <Input
                                        type="longitude"
                                        name="longitude"
                                        id="longitudeValue"
                                        value={longtitudeState}
                                        onChange={e => setLongtitudeState(e.target.value)}
                                        placeholder="x-coordinate" />
                                </FormGroup>
                            </Col>

                            <Col md={4}>
                                <FormGroup>
                                    <Label for="latitudeValue">Latitude</Label>
                                    <Input
                                        type="latitude"
                                        name="latitude"
                                        id="latitudeValue"
                                        value={latitudeState}
                                        onChange={e => setLatitudeState(e.target.value)}
                                        placeholder="y-coordinate" />
                                </FormGroup>
                            </Col>
                        </Row>

                        <Row form>
                            <Col md={4}>
                                <FormGroup>
                                    <Label for="radiusValue">Radius</Label>
                                    <Input
                                        type="radius"
                                        name="radius"
                                        id="radiusValue"
                                        value={radiusState}
                                        onChange={e => setRadiusState(e.target.value)}
                                        min={1}
                                        max={50}
                                    />
                                </FormGroup>
                            </Col>

                            <Col md={4}>
                                <FormGroup>
                                    <Label for="radiusValue">Radius</Label>
                                    <Input
                                        type="range"
                                        value={radiusState}
                                        onChange={e => setRadiusState(e.target.value)}
                                        min={1}
                                        max={51}
                                        step={5}
                                    />
                                    <br />
                                </FormGroup>
                            </Col>
                        </Row>

                        <div>
                            <ButtonToggle color="info" onClick={addPoint}>Add</ButtonToggle>{' '}
                        </div>
                    </Form>

                    <Row>
                        <h3>Points</h3>

                        {routeState.point.map((points, index) => (
                            <ul>{points}</ul>
                        ))}
                    </Row>
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

export default GeometricConfig;
