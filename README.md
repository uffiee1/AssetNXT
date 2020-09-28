|\
 |

SMART ASSET TRACKING & MONITORING

 |\
 |

1.  Introduction

There is a lot of complex IoT and mobile solutions available in the market which are out of reach for small and even mid-sized companies. The goal with the development of "Smart Asset Tracking & Monitoring" is to create conceptual functionality which manages the assets.

As an example, the target company is a small transport business that delivers food and beverage. They are using a device which provides the information from which data they want to manage and monitor the quality of the delivered service. The truck (driver) will be equipped with a smartphone and a Ruvvi environmental sensor: <https://ruuvi.com/> .

1.  Solution outline

1.  Ruuvi Data Collection

The Ruuvi tag will be located in the loading part of the truck. The standard Ruuvi Mobile App will be used to connect the Ruuvi tag and send the collected data as a gateway to a cloud database. The Ruuvi tag can collect telemetry data like temperature, humidity and pressure. The mobile phone is used to collect GPS data.

1.  Cloud

The raw Ruuvi data will be collected in an unstructured cloud database such as MongoDB. The technology will be Cloud independent and able to run on Microsoft Azure Service. The development project will use DockerHub for creating an image and container where we are going to store our version of the project.

1.  Web Application

To visualize and control the data we are going to use React.js as a front-end development technology.

1.  Use cases

Use cases are defined around two types of telemetry data: GPS/ Positions and value-driven telemetry parameters (temperature, humidity, pressure). Both types added value to the business case from the perspective of tracking and monitoring.

1.  Tracking/ Visibility

Tracking is about visualizing the current and past values. For GPS/ Position visibility is about showing current and past positions on a map. For value-driven telemetry parameters visibility is about showing the current numeric values as past values in a graph.

1.  Monitoring/ Control

Monitoring is about managing/ safeguarding values with preset boundaries and alerts on breaches.

For GPS/ Position, this could mean that a current position is mapped against the geographic area, corridor, waypoints & timeslots which can be defined as paths.

For the value-driven telemetry parameters, the current value can be monitored/ controlled against a Service Level Agreement (SLA). An SLA is defined by parameter, minimum, maximum, amount of violations to count as a breach, duration above maximum or below the minimum to count as a breach. The SLA current condition shows if a parameter is currently breached. The SLA Breach shows if a parameter breached over a past period of time.

### Project plan 

- [Client interview]()
- [Improved Project plan based on feedback]()
- [Test cases]() 
- [Software Development Agreement]()


- [Split tasks]()
- [Split tasks Last iteration]()


- [Functional requirements and Use cases]()
- [User reqiurements structure]()
- [Process report]()
- [Presentation]()



### Useful links

[https://ruuvi.com/](https://ruuvi.com/)
