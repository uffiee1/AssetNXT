import React, { useState } from "react";
import "./Navigation.css" 
import logo from "../images/logo.png"
import {
  Navbar,
  NavbarToggler,
  Collapse,
  Nav,
  NavItem,
  NavLink,
  NavbarBrand,
  Row,
  Container,
  Col
} from "reactstrap";
import { Link } from "react-router-dom";

const Navigation = () => {
    const [topbarIsOpen, setTopbarOpen] = useState(true);
    const toggleTopbar = () => {
      setTopbarOpen(!topbarIsOpen);
    }
    
    return (
        <Navbar color="light" light className="navbar shadow-sm m-1 p-2 bg-white rounded" expand="md" >
            <NavbarBrand>
                <NavLink tag={Link} to={"/"}>
                    <Container fluid className="banner-container">
                        <Row className="banner-container-row">
                            <Col className="banner-image-container">
                                <img className="img-fluid" src={logo} />
                            </Col>
                        </Row>
                    </Container>
                </NavLink>
            </NavbarBrand>
                
            <NavbarToggler onClick={toggleTopbar} />

            <Collapse isOpen={topbarIsOpen} navbar>
                <Nav className="mr-auto" navbar>
                    
                    <NavItem  className="mx-3">
                        <NavLink tag={Link} to={"/"}>
                            Home
                        </NavLink>
                    </NavItem>
                    
                    <NavItem className="mx-3">
                        <NavLink tag={Link} to={"/constraints"}>
                            Constraints
                        </NavLink>
                    </NavItem>

                    <NavItem  className="mx-3">
                        <NavLink tag={Link} to={"/notifications"}>
                            Notifications
                        </NavLink>
                    </NavItem>
                    
                    <NavItem  className="mx-3">
                        <NavLink tag={Link} to={"/geo"}>
                            Geometrics
                        </NavLink>
                    </NavItem>

                </Nav>
            </Collapse>
        </Navbar>
    );
}

export default Navigation;