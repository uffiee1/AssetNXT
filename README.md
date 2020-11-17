
## SMART ASSET TRACKING & MONITORING


### 1. Introduction
There is a lot of complex IoT and mobile solutions available in the market which are out of reach for small and even mid-sized companies. The goal with the development of "Smart Asset Tracking & Monitoring" is to create conceptual functionality which manages the assets.

As an example, the target company is a small business that delivers packages. They are using a device which provides the information from which data they want to manage and monitor the quality of the delivered service. The driver will be equipped with a smartphone and a Ruvvi environmental sensor: [https://ruuvi.com/](https://ruuvi.com/).

### 2. Solution outline

#### 2.1. Ruuvi Data Collection

The Ruuvi tag will be located sidewise on the package. The standard Ruuvi Mobile App will be used to connect the Ruuvi tag and send the collected data as a gateway to a cloud database. The Ruuvi tag can collect telemetry data like temperature, humidity and pressure. The mobile phone is used to collect GPS data.

#### 2.2. Cloud
The raw Ruuvi data will be collected in an unstructured cloud database such as MongoDB. The technology will be Cloud independent and able to run on Microsoft Azure Service. The development project will use DockerHub for creating an image and container where we are going to store our version of the project.

#### 2.3. Web Application
To visualize and control the data we are going to use React.js as a front-end development technology.

#### 2.4. Use cases
Use cases are defined around two types of telemetry data: GPS/ Positions and value-driven telemetry parameters (temperature, humidity, pressure). Both types added value to the business case from the perspective of tracking and monitoring.

#### a) Tracking/ Visibility
Tracking is about visualizing the current and past values. For GPS/ Position visibility is about showing current and past positions on a map. For value-driven telemetry parameters visibility is about showing the current numeric values as past values in a graph.

#### b) Monitoring/ Control
Monitoring is about managing/ safeguarding values with preset boundaries and alerts on breaches.

For GPS/ Position, this could mean that a current position is mapped against the geographic area, corridor, waypoints & timeslots which can be defined as paths.

For the value-driven telemetry parameters, the current value can be monitored/ controlled against a Service Level Agreement (SLA). An SLA is defined by parameter, minimum, maximum, amount of violations to count as a breach, duration above maximum or below the minimum to count as a breach. The SLA current condition shows if a parameter is currently breached. The SLA Breach shows if a parameter breached over a past period of time.

 ```json
{
  "tags": [
    {
      "accelX": 0.012,
      "accelY": -0.004,
      "accelZ": 1.008,
      "createDate": "2020-08-11T18:51:58+0300",
      "dataFormat": 5,
      "defaultBackground": 2,
      "favorite": true,
      "humidity": 32.8425,
      "id": "E5:F1:98:34:C0:0F",
      "measurementSequenceNumber": 62865,
      "movementCounter": 21,
      "name": "NYC",
      "pressure": 98702,
      "rssi": -43,
      "temperature": 25.58,
      "txPower": 4,
      "updateAt": "2020-08-18T19:57:48+0300",
      "voltage": 3.013
    }
  ],
  "batteryLevel": 35,
  "deviceId": "yxftd9pnitd-156xhref9g69a",
  "eventId": "c07e3f9f-f6bb-4792-be6f-a9be95cdff38",
  "location": {
    "accuracy": 35.369,
    "latitude": 55.8256671,
    "longitude": 37.5962931
  },
  "time": "2020-08-18T19:57:48+0300"
}
```

### 3. Gateway

With this feature you can let the app act as a gateway, forwarding RuuviTag measurements to a http endpoint using POST requests.

#### 3.1 Setup
The app will act as a gateway when:
- Background scanning is enabled
- A valid URL has been set in app settings

#### 3.1 Data format

Every POST contains 2 blocks: tags data and device information. Tags data can contain one or few tag scan results after "tags". 
If the 2 points are fulfilled the app will POST background scanning results to the URL. The app only sends measurements for tags added in the app

``
    "tags":[{tagdata1},{tagdata2}, ... ,{tagdataN}]
``

#### 3.2 Ruuvi Station gateway API Server

Open source gateway server can be found at: [https://github.com/ruuvi/ruuvi-station-influx-gateway](https://github.com/ruuvi/ruuvi-station-influx-gateway)

More resources about the firmware: [https://docs.ruuvi.com/ruuvi-station-app](https://docs.ruuvi.com/ruuvi-station-app)
